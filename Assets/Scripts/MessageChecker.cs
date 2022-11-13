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
        lblMessage.text = message.Text;
    }

    private void OnTriggerEnter(Collider collision)
    {
        MessagePuzzleManager.instance.addMessage(message);
    }
}
