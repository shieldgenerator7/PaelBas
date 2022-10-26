using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessagePuzzle
{
    public Message message;
    public SleuthTree sleuthTree;

    public MessagePuzzle(Message message)
    {
        this.message = message;
        this.sleuthTree = new SleuthTree(message.Untext);
    }
}
