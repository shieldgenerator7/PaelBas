using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct UndoEntry
{
    public string text;
    public Obfuscator obfuscator;

    public UndoEntry(string text, Obfuscator obfuscator = null)
    {
        this.text = text;
        this.obfuscator = obfuscator;
    }
}
