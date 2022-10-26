using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleuthManager : MonoBehaviour
{
    public static SleuthManager instance;

    private Dictionary<Message, SleuthTree> sleuthTrees = new Dictionary<Message, SleuthTree>();

    private void Awake()
    {
        instance = this;
    }

    public SleuthTree getTree(Message message)
    {
        if (!sleuthTrees.ContainsKey(message))
        {
            sleuthTrees.Add(message, new SleuthTree(message.Untext));
        }
        return sleuthTrees[message];
    }
}
