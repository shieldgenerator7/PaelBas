using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MessageChecker : MonoBehaviour
{
    public Message message;

    public TMP_Text lblMessage;

    private void Start()
    {
        lblMessage.text = MessagePuzzleManager.instance.getMessageText(message);
    }

    private void OnEnable()
    {
        MessagePuzzle mp = MessagePuzzleManager.instance.getMessagePuzzle(message);
        if (mp != null)
        {
            mp.undoStack.onUndoStateChanged += updateSign;
        }
        else
        {
            MessagePuzzleManager.instance.onMessagePuzzleSwitched += registerSign;
        }
    }

    private void OnDisable()
    {
        MessagePuzzle mp = MessagePuzzleManager.instance.getMessagePuzzle(message);
        if (mp != null)
        {
            mp.undoStack.onUndoStateChanged -= updateSign;
        }
        MessagePuzzleManager.instance.onMessagePuzzleSwitched -= registerSign;
    }

    private void updateSign(string text)
    {
        lblMessage.text = text;
    }
    private void registerSign(MessagePuzzle mp)
    {
        if (mp.message == this.message)
        {
            mp.undoStack.onUndoStateChanged += updateSign;
            MessagePuzzleManager.instance.onMessagePuzzleSwitched -= registerSign;
        }
    }
}
