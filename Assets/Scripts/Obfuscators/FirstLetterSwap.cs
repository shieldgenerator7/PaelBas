using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FirstLetterSwap", menuName = "Obfuscator/FirstLetterSwap")]
public class FirstLetterSwap : Obfuscator
{
    public override string obfuscate(string text)
    {
        string[] segments = getSegments(text);
        for (int iS = 0; iS < segments.Length; iS++)
        {
            string segment = segments[iS];
            if (string.IsNullOrWhiteSpace(segment))
            {
                continue;
            }
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
            segments[iS] = setWords(words, segment);
        }
        return setSegments(segments, text);
    }
    public override string unobfuscate(string untext)
    {
        //Interestingly enough,
        //obfuscating it again unobfuscates it
        //in this case
        return obfuscate(untext);
    }
}
