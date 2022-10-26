using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ReverseOrder", menuName = "Obfuscator/ReverseOrder")]
public class ReverseOrder : Obfuscator
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
            for (int iW = 0; iW < words.Length; iW++)
            {
                string word = words[iW];
                if (string.IsNullOrWhiteSpace(word))
                {
                    continue;
                }
                string output = "";
                for (int i = word.Length - 1; i >= 0; i--)
                {
                    output += word[i];
                }
                words[iW] = output;
            }
            segments[iS] = setWords(words, segment);
        }
        return setSegments(segments, text);
    }
    public override string unobfuscate(string untext)
    {
        //it's cool that reversing a reversed string unobfuscates it
        return obfuscate(untext);
    }
}
