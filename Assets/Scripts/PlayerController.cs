using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [Header("Settings")]
    public float moveSpeed = 3;
    public float sprintMoveSpeed = 7;
    public float lookSpeed = 100;
    public float lookSpeedIncrement = 10;
    public float jumpHeight = 3f;
    public float gravityY = -9.81f;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    [Tooltip("How much the scroll wheel changes the position percent of the notebook when closed")]
    public float notebookVisibilityScrollAmount = 0.1f;
    public float maxPickupDistance = 3;

    [Header("Components")]
    public CharacterController characterController;
    public new Camera camera;
    public GameObject notebook;
    public Transform positionOpen;
    public Transform positionClosed;
    public Transform positionClosedVisible;
    public TMP_InputField txtMessage;
    public EventSystem eventSystem;
    public Transform groundCheck;

    private bool showNotebook = false;
    public bool Notebooking => showNotebook;
    private float notebookClosedPositionPercent = 0.0f;//0 = positionClosed; 1 = positionClosedVisible; (0.0-1.0) = position in between
    public float NotebookVisibilityPercent
    {
        get => notebookClosedPositionPercent;
        set
        {
            notebookClosedPositionPercent = Mathf.Clamp(value, 0.0f, 1.0f);
            Debug.Log($"notebookClosedPositionPercent {notebookClosedPositionPercent}");
            notebook.transform.position =
                (positionClosedVisible.position - positionClosed.position)
                * notebookClosedPositionPercent
                + positionClosed.position;
        }
    }

    public int SelectionStart { get; private set; }
    public int SelectionEnd { get; private set; }

    private Transform heldObject;
    private Transform holdPrevParent;
    public bool Holding => heldObject;

    private TMP_Text lblMessage;

    private float rotationX = 0f;
    private float velocityY = 0f;
    private bool isGrounded = true;

    // Start is called before the first frame update
    void Start()
    {
        MessagePuzzleManager.instance.onMessagePuzzleSwitched += updateText;
        MessagePuzzleManager.instance.onMessagePuzzleStateChanged += updateText;
        txtMessage.onValidateInput += (txt, index, chr) =>
        {
            if (chr == '\t')
            {
                return '\0';
            }
            return chr;
        };
        lblMessage = txtMessage.GetComponentInChildren<TMP_Text>();
        txtMessage.onTextSelection.AddListener(selectText);
        updateText(MessagePuzzleManager.instance.MessagePuzzle);
        NotebookVisibilityPercent = 0;//default
        ShowNotebook(showNotebook);
    }

    private void Update()
    {
        if (showNotebook && Input.GetMouseButtonDown(0))
        {
            //2022-11-11: copied from https://stackoverflow.com/a/57691490/2336212
            var charIndex = TMP_TextUtilities.GetCursorIndexFromPosition(lblMessage, Input.mousePosition, camera);

            //Debug.Log($"charIndex: {charIndex}");
            if (charIndex != -1)
            {
                txtMessage.selectionAnchorPosition = charIndex;
                txtMessage.selectionFocusPosition = charIndex + 1;
            }
        }
        if (Input.GetKeyUp(KeyCode.Tab) || (showNotebook && Input.GetKeyUp(KeyCode.Escape)))
        {
            ShowNotebook(!showNotebook);
        }
        //Gravity
        applyGravity();
        //FPS Controls
        if (!showNotebook)
        {
            //Move notebook visibility
            float mouseWheelDelta = Input.mouseScrollDelta.y;
            if (mouseWheelDelta != 0)
            {
                NotebookVisibilityPercent += notebookVisibilityScrollAmount * Mathf.Sign(mouseWheelDelta);
            }
            //Mouse sensivity adjust
            if (Input.GetKeyDown(KeyCode.Minus))
            {
                lookSpeed -= lookSpeedIncrement;
            }
            if (Input.GetKeyDown(KeyCode.Equals))
            {
                lookSpeed += lookSpeedIncrement;
            }
            lookSpeed = Mathf.Clamp(lookSpeed, 10, 200);
            //Move
            moveAndLook();
            interact();
            jump();
        }
    }

    public void ShowNotebook(bool show)
    {
        showNotebook = show;
        notebook.transform.position = ((show) ? positionOpen : positionClosed).position;
        if (showNotebook)
        {
            txtMessage.ActivateInputField();
            eventSystem.SetSelectedGameObject(txtMessage.gameObject);
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            txtMessage.DeactivateInputField();
            eventSystem.SetSelectedGameObject(null);
            Cursor.lockState = CursorLockMode.Locked;
            //put notebook in correct closed position
            NotebookVisibilityPercent = notebookClosedPositionPercent;
        }
    }

    public void adjustMessageIndex(int addend)
    {
        MessagePuzzleManager.instance.MessageIndex += addend;
    }

    public void saveText(string text)
    {
        MessagePuzzleManager.instance.Text = text;
    }
    public void updateText(MessagePuzzle messagePuzzle)
    {
        txtMessage.text = messagePuzzle.Text;
    }

    public void moveAndLook()
    {
        //2022-11-12: made with help from https://youtu.be/_QajrabyTJc

        //Sprint
        bool sprinting = Input.GetButton("Sprint");
        float speed = (sprinting) ? sprintMoveSpeed : moveSpeed;

        //Move
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        if (horizontal != 0 || vertical != 0)
        {
            Vector3 moveDir = transform.forward * vertical + transform.right * horizontal;
            characterController.Move(moveDir * speed * Time.deltaTime);
        }

        //Look
        float mouseX = Input.GetAxisRaw("Mouse X");
        float mouseY = Input.GetAxisRaw("Mouse Y");
        if (mouseX != 0)
        {
            transform.Rotate(Vector3.up * mouseX * lookSpeed * Time.deltaTime);
        }
        if (mouseY != 0)
        {
            rotationX -= mouseY * lookSpeed * Time.deltaTime;
            rotationX = Mathf.Clamp(rotationX, -90f, 90f);
            camera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        }
    }

    public void interact()
    {
        //Pick up or drop object
        if (Input.GetMouseButtonDown(0))
        {
            if (heldObject)
            {
                //drop it
                heldObject.GetComponent<Rigidbody>().isKinematic = false;
                heldObject.parent = holdPrevParent;
                holdPrevParent = null;
                heldObject = null;
            }
            else
            {
                //Find object to hold
                Interactible interactible = null;
                RaycastHit[] infos;
                infos = Physics.RaycastAll(camera.transform.position, camera.transform.forward, maxPickupDistance);
                for (int i = 0; i < infos.Length; i++)
                {
                    RaycastHit info = infos[i];
                    interactible = info.transform.GetComponent<Interactible>();
                    if (interactible)
                    {
                        break;
                    }
                }
                //interact with it
                if (interactible)
                {
                    //Note it
                    if (interactible.notable)
                    {
                        //Add message
                        MessagePuzzleManager mpm = MessagePuzzleManager.instance;
                        Message message = interactible.GetComponent<MessageChecker>().message;
                        mpm.addMessage(message);
                        ShowNotebook(true);
                    }
                    //Hold it
                    else if (interactible.holdable)
                    {
                        heldObject = interactible.transform;
                        heldObject.GetComponent<Rigidbody>().isKinematic = true;
                        holdPrevParent = heldObject.parent;
                        heldObject.parent = camera.transform;
                    }
                }
            }
        }
        //Move held object
        if (heldObject)
        {
            //keep object right-side up
            Vector3 euler = heldObject.eulerAngles;
            heldObject.eulerAngles = new Vector3(0, euler.y, 0);
        }
    }

    public void applyGravity()
    {
        velocityY += gravityY * Time.deltaTime;

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocityY < 0)
        {
            velocityY = -2f;
        }

        characterController.Move(Vector3.up * velocityY * Time.deltaTime);
    }

    public void jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocityY = Mathf.Sqrt(jumpHeight * -2f * gravityY);
            characterController.Move(Vector3.up * velocityY * Time.deltaTime);
        }
    }

    public void selectText(string str, int pos1, int pos2)
    {
        if (txtMessage.isFocused)
        {
            SelectionStart = Mathf.Min(pos1, pos2);
            SelectionEnd = Mathf.Max(pos1 - 1, pos2 - 1);
            Debug.Log($"selectText: {str}, ({SelectionStart} - {SelectionEnd})/{txtMessage.text.Length}");
        }
    }


}
