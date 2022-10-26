using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessagePuzzleManager : MonoBehaviour
{
    public static MessagePuzzleManager instance;

    public List<Message> messages = new List<Message>();

    private MessagePuzzleData data = new MessagePuzzleData();

    private int messageIndex = 0;
    private MessagePuzzle messagePuzzle;

    public int MessageIndex
    {
        get => messageIndex;
        set
        {
            //Switch to new message
            messageIndex = Mathf.Clamp(value, 0, messages.Count - 1);
            messagePuzzle = data.getMessagePuzzle(messages[messageIndex]);
            onMessageSwitched?.Invoke(messagePuzzle.message);
        }
    }
    public delegate void MessageSwitched(Message m);
    public MessageSwitched onMessageSwitched;

    public string Text
    {
        get => messagePuzzle.sleuthTree.Text;
        set => messagePuzzle.sleuthTree.saveText(value);
    }

    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
        //Init text
        MessageIndex = 0;
    }

    public void pushObfuscator(Obfuscator obf)
    {
        messagePuzzle.sleuthTree.pushObfuscator(obf);
        onObfuscatorPushed?.Invoke(obf);
    }
    public delegate void ObfuscatorPushed(Obfuscator obf);
    public ObfuscatorPushed onObfuscatorPushed;

    public void popObfuscator()
    {
        messagePuzzle.sleuthTree.popObfuscator();
        onObfuscatorPopped?.Invoke();
    }
    public delegate void ObfuscatorPopped();
    public ObfuscatorPopped onObfuscatorPopped;

}
