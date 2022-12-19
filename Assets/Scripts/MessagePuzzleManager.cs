using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessagePuzzleManager : MonoBehaviour
{
    public static MessagePuzzleManager instance;

    [Tooltip("The max amount of obfuscator levels per message")]
    public int maxObfuscatorLayers = 3;

    public List<Message> messages = new List<Message>();

    private MessagePuzzleData data = new MessagePuzzleData();

    private int messageIndex = -1;
    private MessagePuzzle messagePuzzle;

    public int MessageIndex
    {
        get => messageIndex;
        set
        {
            if (messages.Count == 0)
            {
                messageIndex = -1;
                return;
            }
            //Switch to new message
            messageIndex = Mathf.Clamp(value, 0, messages.Count - 1);
            messagePuzzle = data.getMessagePuzzle(messages[messageIndex]);
            onMessageSwitched?.Invoke(messagePuzzle);
        }
    }
    public delegate void OnMessagePuzzle(MessagePuzzle m);
    public OnMessagePuzzle onMessageSwitched;

    public string Text
    {
        get => messagePuzzle.undoStack.Text;
        set => messagePuzzle.undoStack.recordState(value);
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
        string unobfStr = obf.Unobfuscate(messagePuzzle.undoStack.Text);
        messagePuzzle.undoStack.recordState(unobfStr, obf);
        onObfuscatorPushed?.Invoke(obf);
    }
    public delegate void ObfuscatorPushed(Obfuscator obf);
    public ObfuscatorPushed onObfuscatorPushed;

    public void popObfuscator()
    {
        messagePuzzle.undoStack.undo();
        onObfuscatorPopped?.Invoke();
    }
    public delegate void ObfuscatorPopped();
    public ObfuscatorPopped onObfuscatorPopped;

    public void addMessage(Message message)
    {
        if (!hasMessage(message))
        {
            messages.Add(message);
            onMessageAdded?.Invoke(data.getMessagePuzzle(message));
        }
        MessageIndex = messages.IndexOf(message);
    }
    public OnMessagePuzzle onMessageAdded;

    public bool hasMessage(Message message) => messages.Contains(message);

    public string getMessageText(Message message)
    {
        return data.getMessagePuzzle(message).undoStack.Text;
    }
    public MessagePuzzle getMessagePuzzle(Message message)
    {
        return data.getMessagePuzzle(message);
    }

}
