using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MissingLetter", menuName = "Obfuscator/MissingLetter")]
public class MissingLetter : Obfuscator
{
    [Range(1, 10)]
    [Tooltip("The position of the letter to remove. Base 1")]
    public int missingLetterPosition = 3;
    [Tooltip("The character to use to fill in for the missing character when unobfuscating")]
    public string missingFillerChar = "?";

    protected override ObfuscationScope Scope => ObfuscationScope.WORD;

    protected override string obfuscate(string word)
    {
                if (word.Length < missingLetterPosition)
                {
                    return word;
                }
                string head = word.Substring(0, missingLetterPosition - 1);
                string tail = (word.Length > missingLetterPosition)
                    ? word.Substring(missingLetterPosition)
                    : "";
                return head + tail;
    }
    protected override string unobfuscate(string word)
    {
        int prevLetterPosition = missingLetterPosition - 1;
        
                if (word.Length < prevLetterPosition)
                {
                    return word;
                }
                string head = word.Substring(0, missingLetterPosition - 1);
                string middle = missingFillerChar;
                string tail = (word.Length > prevLetterPosition)
                    ? word.Substring(prevLetterPosition)
                    : "";
                return head + middle + tail;
    }
}
