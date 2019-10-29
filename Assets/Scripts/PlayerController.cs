using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [TextArea]
    public string currentText;

    public Obfuscator nextFilter;
    public bool pop;

    [SerializeField]
    private int messageIndex = 0;
    [SerializeField]
    private List<Message> messages;

    public int MessageIndex
    {
        get => messageIndex;
        set
        {
            messageIndex = Mathf.Min(
                messages.Count - 1,
                Mathf.Max(
                    0,
                    value
                    )
                );
            txtMessage.text = messages[messageIndex].CurrentText;
        }
    }

    public Message CurrentMessage
    {
        get => messages[MessageIndex];
        set => MessageIndex = messages.IndexOf(value);
    }

    public TMP_InputField txtMessage;
    
    // Start is called before the first frame update
    void Start()
    {
        //string[] result = "...a.b. c.d.".Split('.', ' ');
        //Debug.Log("result: " + result.Length + " " + result);
        //for (int i = 0; i < result.Length; i++)
        //{
        //    Debug.Log("r[i]: " + result[i]
        //        + " ==" + (result[i] == "")
        //        );
        //}
        currentText = messages[messageIndex].Untext;
        MessageIndex = 0;
        txtMessage.ActivateInputField();
    }

    // Update is called once per frame
    void Update()
    {
        if (nextFilter != null)
        {
            currentText = CurrentMessage.pushSleuthNode(currentText, nextFilter);
            nextFilter = null;
        }
        if (pop)
        {
            currentText = CurrentMessage.popSleuthNode(currentText);
            pop = false;
        }
    }

    public void adjustMessageIndex(int addend)
    {
        MessageIndex += addend;
    }


}
