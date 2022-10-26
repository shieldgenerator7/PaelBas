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

    private int messageIndex = -1;
    [SerializeField]
    private List<Message> messages = new List<Message>();

    public int MessageIndex
    {
        get => messageIndex;
        set
        {
            //Save prev message
            if (messageIndex >= 0)
            {
                CurrentMessage.SleuthTree.saveText(Text);
            }
            //Switch to new message
            messageIndex = Mathf.Clamp(value, 0, messages.Count - 1);
            Text = messages[messageIndex].SleuthTree.Text;
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
        //Init messages
        messages.ForEach(msg => msg.init());
        //Init text
        MessageIndex = 0;
        txtMessage.ActivateInputField();
    }

    public void adjustMessageIndex(int addend)
    {
        MessageIndex += addend;
    }

    public void pushObfuscator(Obfuscator obf)
    {
        SleuthTree tree = CurrentMessage.SleuthTree;
        tree.saveText(Text);
        Text = tree.pushObfuscator(obf);
        obfuscatorPushed?.Invoke(obf);
    }

    public void popObfuscator()
    {
        SleuthTree tree = CurrentMessage.SleuthTree;
        tree.saveText(Text);
        Text = tree.popObfuscator();
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
