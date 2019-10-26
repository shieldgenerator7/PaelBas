using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseOrder : Obfuscator
{
    public override string obfuscate(string text)
    {
        string output = "";
        for (int i = text.Length - 1; i >= 0; i--)
        {
            output += text[i];
        }
        return output;
    }
    public override string unobfuscate(string untext)
    {
        //it's cool that reversing a reversed string unobfuscates it
        return obfuscate(untext);
    }
}
