﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddObfuscatorButton : MonoBehaviour
{
    public Obfuscator obfuscator;

    public void addObfuscator()
    {
        MessageManager.instance.pushObfuscator(obfuscator);
    }
}
