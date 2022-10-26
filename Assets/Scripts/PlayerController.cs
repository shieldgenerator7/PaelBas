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
        MessageManager.instance.onMessageSwitched += (msg) => updateText();
        MessageManager.instance.onObfuscatorPushed += (obf) => updateText();
        MessageManager.instance.onObfuscatorPopped += updateText;
        txtMessage.ActivateInputField();
    }

    public void adjustMessageIndex(int addend)
    {
        MessageManager.instance.MessageIndex += addend;
    }

    public void saveText(string text)
    {
        MessageManager.instance.Text = text;
    }
    public void updateText()
    {
        txtMessage.text = MessageManager.instance.Text;
    }


}
