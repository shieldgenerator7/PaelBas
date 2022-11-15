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
            mp.sleuthTree.onSolutionChanged += updateSign;
        }
        else
        {
            MessagePuzzleManager.instance.onMessageSwitched += registerSign;
        }
    }

    private void OnDisable()
    {
        MessagePuzzle mp = MessagePuzzleManager.instance.getMessagePuzzle(message);
        if (mp != null)
        {
            mp.sleuthTree.onSolutionChanged -= updateSign;
        }
        MessagePuzzleManager.instance.onMessageSwitched -= registerSign;
    }

    private void updateSign(string text)
    {
        lblMessage.text = text;
    }
    private void registerSign(MessagePuzzle mp)
    {
        if (mp.message == this.message)
        {
            mp.sleuthTree.onSolutionChanged += updateSign;
            MessagePuzzleManager.instance.onMessageSwitched -= registerSign;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        MessagePuzzleManager.instance.addMessage(message);
    }
}
