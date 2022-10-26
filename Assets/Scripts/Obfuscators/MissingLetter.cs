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
                if (string.IsNullOrWhiteSpace(word) || word.Length < missingLetterPosition)
                {
                    continue;
                }
                string head = word.Substring(0, missingLetterPosition - 1);
                string tail = (word.Length > missingLetterPosition)
                    ? word.Substring(missingLetterPosition)
                    : "";
                words[iW] = head + tail;
            }
            segments[iS] = setWords(words, segment);
        }
        return setSegments(segments, text);
    }
    public override string unobfuscate(string untext)
    {
        int prevLetterPosition = missingLetterPosition - 1;
        string[] segments = getSegments(untext);
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
                if (string.IsNullOrWhiteSpace(word) || word.Length < prevLetterPosition)
                {
                    continue;
                }
                string head = word.Substring(0, missingLetterPosition - 1);
                string middle = missingFillerChar;
                string tail = (word.Length > prevLetterPosition)
                    ? word.Substring(prevLetterPosition)
                    : "";
                words[iW] = head + middle + tail;
            }
            segments[iS] = setWords(words, segment);
        }
        return setSegments(segments, untext);
    }
}
