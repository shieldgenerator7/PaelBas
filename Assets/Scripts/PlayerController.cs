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
        MessagePuzzleManager.instance.onMessageSwitched += (msg) => updateText();
        MessagePuzzleManager.instance.onObfuscatorPushed += (obf) => updateText();
        MessagePuzzleManager.instance.onObfuscatorPopped += updateText;
        txtMessage.ActivateInputField();
        updateText();
    }

    public void adjustMessageIndex(int addend)
    {
        MessagePuzzleManager.instance.MessageIndex += addend;
    }

    public void saveText(string text)
    {
        MessagePuzzleManager.instance.Text = text;
    }
    public void updateText()
    {
        txtMessage.text = MessagePuzzleManager.instance.Text;
    }


}
