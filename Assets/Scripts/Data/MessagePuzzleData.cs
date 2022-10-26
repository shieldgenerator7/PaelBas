using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class MessagePuzzleData
{
    private Dictionary<Message, MessagePuzzle> messagePuzzleMap = new Dictionary<Message, MessagePuzzle>();

    public MessagePuzzle getMessagePuzzle(Message message)
    {
        if (!messagePuzzleMap.ContainsKey(message))
        {
            addMessage(message);
        }
        return messagePuzzleMap[message];
    }

    private MessagePuzzle addMessage(Message message)
    {
        MessagePuzzle mp = new MessagePuzzle(message);
        messagePuzzleMap.Add(message, mp);
        return mp;
    }


    #region Saving and Loading

    // used for saving it out
    private MessagePuzzle[] messagePuzzleList;

    //call before saving
    public void compress()
    {
        messagePuzzleList = new MessagePuzzle[messagePuzzleMap.Count];
        int i = 0;
        foreach (Message msg in messagePuzzleMap.Keys)
        {
            messagePuzzleList[i] = messagePuzzleMap[msg];
            i++;
        }
    }

    //call after loading
    public void inflate()
    {
        foreach (MessagePuzzle mp in messagePuzzleList)
        {
            messagePuzzleMap.Add(mp.message, mp);
        }
    }

    #endregion
}
