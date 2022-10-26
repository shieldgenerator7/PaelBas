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
        //Swap positions
        for (int i = word.Length - 1; i >= 0; i--)
        {
            output += word[i];
        }
        //Swap case
        if (output.Length >= 2)
        {
            char first = output[0];
            char last = output[output.Length - 1];
            (first, last) = Utility.swapCase(first, last);
            output = first + output.Substring(1, output.Length - 2) + last;
        }
        //Return
        return output;
    }
    protected override string unobfuscate(string untext)
    {
        //it's cool that reversing a reversed string unobfuscates it
        return obfuscate(untext);
    }
}
