using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessagePuzzleManager : MonoBehaviour
{
    public static MessagePuzzleManager instance;

    public List<Message> messages = new List<Message>();

    private MessagePuzzleData data = new MessagePuzzleData();

    private int messageIndex = -1;
    private MessagePuzzle messagePuzzle;
    public MessagePuzzle MessagePuzzle => messagePuzzle;

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
            onMessagePuzzleSwitched?.Invoke(messagePuzzle);
        }
    }

    public delegate void MessagePuzzleDelegate(MessagePuzzle m);
    public MessagePuzzleDelegate onMessagePuzzleSwitched;
    public MessagePuzzleDelegate onMessagePuzzleStateChanged;

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

    public void useObfuscator(Obfuscator obf)
    {
        string unobfStr = obf.Unobfuscate(messagePuzzle.undoStack.Text);
        messagePuzzle.undoStack.recordState(unobfStr, obf);
        onMessagePuzzleStateChanged?.Invoke(messagePuzzle);
    }
    public void undo()
    {
        messagePuzzle.undoStack.undo();
        onMessagePuzzleStateChanged?.Invoke(messagePuzzle);
    }
    public void redo()
    {
        messagePuzzle.undoStack.redo();
        onMessagePuzzleStateChanged?.Invoke(messagePuzzle);
    }

    public void addMessage(Message message)
    {
        if (!hasMessage(message))
        {
            messages.Add(message);
            onMessageAdded?.Invoke(data.getMessagePuzzle(message));
        }
        MessageIndex = messages.IndexOf(message);
    }
    public MessagePuzzleDelegate onMessageAdded;

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
