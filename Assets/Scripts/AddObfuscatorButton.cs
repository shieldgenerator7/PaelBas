using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddObfuscatorButton : MonoBehaviour
{
    public Obfuscator obfuscator;

    public void addObfuscator()
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        player.pushObfuscator(obfuscator);
    }
}
