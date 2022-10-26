using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FirstLetterSwap", menuName = "Obfuscator/FirstLetterSwap")]
public class FirstLetterSwap : Obfuscator
{
    protected override ObfuscationScope Scope => ObfuscationScope.SEGMENT;

    protected override string obfuscate(string segment)
    {
        string[] words = getWords(segment);
        int swapIndex = -1;
        for (int iW = 0; iW < words.Length; iW++)
        {
            string word = words[iW];
            if (string.IsNullOrWhiteSpace(word))
            {
                continue;
            }
            if (swapIndex < 0)
            {
                swapIndex = iW;
            }
            else
            {
                char swapChar = word[0];
                words[iW] = words[swapIndex][0] + word.Substring(1);
                words[swapIndex] = swapChar + words[swapIndex].Substring(1);
                swapIndex = -1;
            }
        }
        return setWords(words, segment);
    }
    protected override string unobfuscate(string untext)
    {
        //Interestingly enough,
        //obfuscating it again unobfuscates it
        //in this case
        return obfuscate(untext);
    }
}
