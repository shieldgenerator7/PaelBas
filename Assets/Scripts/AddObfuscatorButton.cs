using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddObfuscatorButton : MonoBehaviour
{
    public Obfuscator obfuscator;

    public void addObfuscator()
    {
        MessagePuzzleManager.instance.useObfuscator(obfuscator);
        FindObjectOfType<PlayerController>().reselectText();
    }
}
