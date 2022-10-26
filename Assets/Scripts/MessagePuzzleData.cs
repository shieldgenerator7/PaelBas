using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessagePuzzleData
{
    [SerializeField]
    private Dictionary<Message, SleuthTree> sleuthTreeList = new Dictionary<Message, SleuthTree>();
    [SerializeField]
    public List<Message> messages = new List<Message>();

    public SleuthTree getTree(Message message)
    {
        if (!sleuthTreeList.ContainsKey(message))
        {
            sleuthTreeList.Add(message, new SleuthTree(message.Untext));
        }
        return sleuthTreeList[message];
    }
}
