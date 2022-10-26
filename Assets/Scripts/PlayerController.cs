using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [TextArea]
    private string currentText;
    public string Text
    {
        get => currentText;
        set
        {
            currentText = value;
            txtMessage.text = currentText;
        }
    }

    [SerializeField]
    private int messageIndex = 0;
    [SerializeField]
    private List<Message> messages = new List<Message>();

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
            Text = messages[messageIndex].CurrentText;
            messageSwitched?.Invoke(CurrentMessage);
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
        Text = messages[messageIndex].Untext;
        MessageIndex = 0;
        txtMessage.ActivateInputField();
    }

    public void adjustMessageIndex(int addend)
    {
        MessageIndex += addend;
    }

    public void pushObfuscator(Obfuscator obf)
    {
        Text = CurrentMessage.pushSleuthNode(Text, obf);
        obfuscatorPushed?.Invoke(obf);
    }

    public void popObfuscator()
    {
        Text = CurrentMessage.popSleuthNode(Text);
        obfuscatorPopped?.Invoke();
    }

    //
    // DELEGATES
    //
    public delegate void ObfuscatorPushed(Obfuscator obf);
    public ObfuscatorPushed obfuscatorPushed;

    public delegate void ObfuscatorPopped();
    public ObfuscatorPopped obfuscatorPopped;

    public delegate void MessageSwitched(Message m);
    public MessageSwitched messageSwitched;

}
