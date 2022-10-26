using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageManager : MonoBehaviour
{
    public static MessageManager instance;

    private int messageIndex = 0;
   

    public int MessageIndex
    {
        get => messageIndex;
        set
        {
            //Switch to new message
            messageIndex = Mathf.Clamp(value, 0, messages.Count - 1);
            onMessageSwitched?.Invoke(CurrentMessage);
        }
    }
    public delegate void MessageSwitched(Message m);
    public MessageSwitched onMessageSwitched;

    public Message CurrentMessage
    {
        get => messages[MessageIndex];
        set => MessageIndex = messages.IndexOf(value);
    }

    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
        //Init messages
        messages.ForEach(msg => msg.init());
        //Init text
        MessageIndex = 0;
    }
}
