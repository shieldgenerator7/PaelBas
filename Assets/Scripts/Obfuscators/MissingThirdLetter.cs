using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissingThirdLetter : Obfuscator
{
    public string missingFillerChar = "?";

    public override string obfuscate(string text)
    {
        string[] segments = getSegments(text);
        for (int iS = 0; iS < segments.Length; iS++)
        {
            string segment = segments[iS];
            if (segment == "")
            {
                continue;
            }
            string[] words = getWords(segment);
            for (int iW = 0; iW < words.Length; iW++)
            {
                string word = words[iW];
                if (word == "" || word.Length < 3)
                {
                    continue;
                }
                string head = "" + word[0] + word[1];
                string tail = (word.Length > 3)
                    ? word.Substring(3)
                    : "";
                words[iW] = head + tail;
            }
            segments[iS] = setWords(words, segment);
        }
        return setSegments(segments, text);
    }
    public override string unobfuscate(string untext)
    {
        string[] segments = getSegments(untext);
        for (int iS = 0; iS < segments.Length; iS++)
        {
            string segment = segments[iS];
            if (segment == "")
            {
                continue;
            }
            string[] words = getWords(segment);
            for (int iW = 0; iW < words.Length; iW++)
            {
                string word = words[iW];
                if (word == "" || word.Length < 2)
                {
                    continue;
                }
                string head = "" + word[0] + word[1];
                string middle = missingFillerChar;
                string tail = (word.Length > 2)
                    ? word.Substring(2)
                    : "";
                words[iW] = head + middle + tail;
            }
            segments[iS] = setWords(words, segment);
        }
        return setSegments(segments, untext);
    }
}
