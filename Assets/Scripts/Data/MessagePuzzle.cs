using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessagePuzzle
{
    public Message message;
    public UndoStack undoStack;

    public MessagePuzzle(Message message)
    {
        this.message = message;
        this.undoStack = new UndoStack(message.Untext);
    }
}
