using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{  

    public TMP_InputField txtMessage;

    private TMP_Text lblMessage;

    // Start is called before the first frame update
    void Start()
    {
        MessagePuzzleManager.instance.onMessageSwitched += (msg) => updateText();
        MessagePuzzleManager.instance.onObfuscatorPushed += (obf) => updateText();
        MessagePuzzleManager.instance.onObfuscatorPopped += updateText;
        txtMessage.ActivateInputField();
        lblMessage = txtMessage.GetComponentInChildren<TMP_Text>();
        updateText();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //2022-11-11: copied from https://stackoverflow.com/a/57691490/2336212
            var charIndex = TMP_TextUtilities.FindIntersectingCharacter(lblMessage, Input.mousePosition, Camera.main, false);

            //Debug.Log($"charIndex: {charIndex}");
            if (charIndex != -1)
            {
                char clickedChar = lblMessage.textInfo.characterInfo[charIndex].character;
                if (clickedChar == '?' || clickedChar == '_')
                {
                    txtMessage.selectionAnchorPosition = charIndex;
                    txtMessage.selectionFocusPosition = charIndex + 1;
                }
            }
        }
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
