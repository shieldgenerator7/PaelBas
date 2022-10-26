using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ReverseOrder", menuName = "Obfuscator/ReverseOrder")]
public class ReverseOrder : Obfuscator
{
    protected override ObfuscationScope Scope => ObfuscationScope.WORD;

    protected override string obfuscate(string word)
    {
        string output = "";
        for (int i = word.Length - 1; i >= 0; i--)
        {
            output += word[i];
        }
        return output;
    }
    protected override string unobfuscate(string untext)
    {
        //it's cool that reversing a reversed string unobfuscates it
        return obfuscate(untext);
    }
}
