using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageManager : MonoBehaviour
{
    public static MessageManager instance;

    [SerializeField]
    private MessagePuzzleData data;

    private int messageIndex = 0;
    private SleuthTree sleuthTree;

    public int MessageIndex
    {
        get => messageIndex;
        set
        {
            //Switch to new message
            messageIndex = Mathf.Clamp(value, 0, data.messages.Count - 1);
            sleuthTree = data.getTree(data.messages[messageIndex]);
            onMessageSwitched?.Invoke(CurrentMessage);
        }
    }
    public delegate void MessageSwitched(Message m);
    public MessageSwitched onMessageSwitched;

    public Message CurrentMessage
    {
        get => data.messages[MessageIndex];
        set => MessageIndex = data.messages.IndexOf(value);
    }

    public string Text
    {
        get => sleuthTree.Text;
        set => sleuthTree.saveText(value);
    }

    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
        //Init messages
        data.messages.ForEach(msg => msg.init());
        //Init text
        MessageIndex = 0;
    }

    public void pushObfuscator(Obfuscator obf)
    {
        sleuthTree.pushObfuscator(obf);
        onObfuscatorPushed?.Invoke(obf);
    }
    public delegate void ObfuscatorPushed(Obfuscator obf);
    public ObfuscatorPushed onObfuscatorPushed;

    public void popObfuscator()
    {
        sleuthTree.popObfuscator();
        onObfuscatorPopped?.Invoke();
    }
    public delegate void ObfuscatorPopped();
    public ObfuscatorPopped onObfuscatorPopped;

}
