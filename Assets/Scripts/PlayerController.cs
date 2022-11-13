﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [Header("Settings")]
    public float moveSpeed = 3;
    public float lookSpeed = 100;
    public float gravityY = -9.81f;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    [Header("Components")]
    public CharacterController characterController;
    public Camera camera;
    public GameObject notebook;
    public TMP_InputField txtMessage;
    public EventSystem eventSystem;
    public Transform groundCheck;

    private bool showNotebook = true;

    private TMP_Text lblMessage;

    private float rotationX = 0f;
    private float velocityY = 0f;

    // Start is called before the first frame update
    void Start()
    {
        MessagePuzzleManager.instance.onMessageSwitched += (msg) => updateText();
        MessagePuzzleManager.instance.onObfuscatorPushed += (obf) => updateText();
        MessagePuzzleManager.instance.onObfuscatorPopped += updateText;
        txtMessage.ActivateInputField();
        lblMessage = txtMessage.GetComponentInChildren<TMP_Text>();
        updateText();
        txtMessage.onValidateInput += (txt, index, chr) =>
        {
            if (chr == '\t')
            {
                return '\0';
            }
            return chr;
        };
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //2022-11-11: copied from https://stackoverflow.com/a/57691490/2336212
            var charIndex = TMP_TextUtilities.GetCursorIndexFromPosition(lblMessage, Input.mousePosition, Camera.main);

            //Debug.Log($"charIndex: {charIndex}");
            if (charIndex != -1)
            {
                txtMessage.selectionAnchorPosition = charIndex;
                txtMessage.selectionFocusPosition = charIndex + 1;
            }
        }
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            showNotebook = !showNotebook;
            notebook.SetActive(showNotebook);
            txtMessage.enabled = showNotebook;
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
            }
        }
        //Gravity
        applyGravity();
        //FPS Controls
        if (!showNotebook)
        {
            moveAndLook();
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
    public void updateText()
    {
        txtMessage.text = MessagePuzzleManager.instance.Text;
    }

    public void moveAndLook()
    {
        //2022-11-12: made with help from https://youtu.be/_QajrabyTJc

        //Move
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        if (horizontal != 0 || vertical != 0)
        {
            Vector3 moveDir = transform.forward * vertical + transform.right * horizontal;
            characterController.Move(moveDir * moveSpeed * Time.deltaTime);
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

    public void applyGravity()
    {
        velocityY += gravityY * Time.deltaTime;

        bool isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocityY < 0)
        {
            velocityY = -2f;
        }

        characterController.Move(Vector3.up * velocityY * Time.deltaTime);
    }

}
