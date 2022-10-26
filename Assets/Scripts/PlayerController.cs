using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{  

    public TMP_InputField txtMessage;

    // Start is called before the first frame update
    void Start()
    {
        MessageManager.instance.onMessageSwitched += (msg) =>
        {
            txtMessage.text = SleuthManager.instance.getTree(msg).Text;
        };
        SleuthManager.instance.onObfuscatorPushed += (obf) =>
        {
            txtMessage.text = SleuthManager.instance.getTree(
                MessageManager.instance.CurrentMessage
                ).Text;
        };
        SleuthManager.instance.onObfuscatorPopped += () =>
        {
            txtMessage.text = SleuthManager.instance.getTree(
                MessageManager.instance.CurrentMessage
                ).Text;
        };
        txtMessage.ActivateInputField();
    }

    public void adjustMessageIndex(int addend)
    {
        MessageManager.instance.MessageIndex += addend;
    }

    public void saveText(string text)
    {
        SleuthManager.instance.saveText(text);
    }


}
