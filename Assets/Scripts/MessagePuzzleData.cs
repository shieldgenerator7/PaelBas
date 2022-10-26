using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MessagePuzzleData
{
    private List<MessagePuzzle> messagePuzzles = new List<MessagePuzzle>();

    public bool hasIndex(int index)
        => index >= 0 && index < messagePuzzles.Count;

    public MessagePuzzle getMessagePuzzleByIndex(int index)
    {
        if (hasIndex(index))
        {
            return messagePuzzles[index];
        }
        return null;
    }

    public MessagePuzzle addMessage(Message message)
    {
        MessagePuzzle mp = new MessagePuzzle(message);
        messagePuzzles.Add(mp);
        return mp;
    }
}
