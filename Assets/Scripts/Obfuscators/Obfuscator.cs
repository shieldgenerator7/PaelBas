using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Obfuscator : ScriptableObject
{
    public string characterName;

    public string ObfuscatedCharacterName
        => (!string.IsNullOrWhiteSpace(characterName))
            ? Obfuscate(characterName)
            : null;

    protected enum ObfuscationScope
    {
        MESSAGE,
        SEGMENT,
        WORD,
        LETTER,
    }
    protected abstract ObfuscationScope Scope { get; }

    private static char[] segmentDelimiters = new char[]
    {
        '.',
        '?',
        '!',
        '\n',
        '\r'
    };
    private static char[] wordDelimiters = new char[]
    {
        '.',
        '?',
        '!',
        '\n',
        '\r',
        ',',
        ' '
    };

    public string Obfuscate(string message)
    {
        if (Scope == ObfuscationScope.MESSAGE)
        {
            //Obfuscate entire message
            return obfuscate(message);
        }
        string[] segments = getSegments(message);
        for (int iS = 0; iS < segments.Length; iS++)
        {
            string segment = segments[iS];
            if (string.IsNullOrWhiteSpace(segment))
            {
                continue;
            }
            if (Scope == ObfuscationScope.SEGMENT)
            {
                //Obfuscate each segment individually
                segments[iS] = obfuscate(segment);
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
                if (Scope == ObfuscationScope.WORD)
                {
                    //Obfuscate each word individually
                    words[iW] = obfuscate(word);
                    continue;
                }
                if (Scope == ObfuscationScope.LETTER)
                {
                    string newWord = "";
                    for (int iL = 0; iL < word.Length; iL++)
                    {
                        newWord += obfuscate(word[iL].ToString());
                    }
                    words[iW] = newWord;
                }
                else
                {
                    Debug.LogError($"Unknown obfuscation scope! scope: {Scope}");
                }
            }
            segments[iS] = setWords(words, segment);
        }
        return setSegments(segments, message);

    }

    public string Unobfuscate(string message, int startIndex = 0, int endIndex = -1)
    {
        //
        if (startIndex < 0)
        {
            startIndex = 0;
        }
        if (endIndex < 0)
        {
            endIndex = message.Length - 1;
        }
        string submessage = message.Substring(startIndex, endIndex - startIndex + 1);
        //
        if (Scope == ObfuscationScope.MESSAGE)
        {
            //Obfuscate entire message
            return unobfuscate(submessage);
        }
        string[] segments = getSegments(submessage);
        for (int iS = 0; iS < segments.Length; iS++)
        {
            string segment = segments[iS];
            if (string.IsNullOrWhiteSpace(segment))
            {
                continue;
            }
            if (Scope == ObfuscationScope.SEGMENT)
            {
                //Obfuscate each segment individually
                segments[iS] = unobfuscate(segment);
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
                if (Scope == ObfuscationScope.WORD)
                {
                    //Obfuscate each word individually
                    words[iW] = unobfuscate(word);
                    continue;
                }
                if (Scope == ObfuscationScope.LETTER)
                {
                    string newWord = "";
                    for (int iL = 0; iL < word.Length; iL++)
                    {
                        newWord += unobfuscate(word[iL].ToString());
                    }
                    words[iW] = newWord;
                }
                else
                {
                    Debug.LogError($"Unknown obfuscation scope! scope: {Scope}");
                }
            }
            segments[iS] = setWords(words, segment);
        }
        submessage = setSegments(segments, submessage);
        string newmessage = message.Substring(0, startIndex) + submessage;
        if (endIndex < message.Length - 1)
        {
            newmessage += message.Substring(endIndex + 1, message.Length - 1 - endIndex);
        }
        return newmessage;

    }

    protected abstract string obfuscate(string text);

    protected abstract string unobfuscate(string untext);

    public string[] getSegments(string text)
        => text.Split(segmentDelimiters);

    public string[] getWords(string text)
        => text.Split(wordDelimiters);

    /// <summary>
    /// Inserts the segments into the text
    /// </summary>
    /// <param name="segments"></param>
    /// <param name="text"></param>
    /// <returns></returns>
    public string setSegments(string[] segments, string text)
    {
        string output = "";
        int segmentIndex = 0;
        for (int i = 0; i < text.Length; i++)
        {
            if (segmentDelimiters.Contains(text[i]))
            {
                //Segment delimiter found
                output += segments[segmentIndex] + text[i];
                segmentIndex++;
            }
        }
        output += segments[segmentIndex];
        return output;
    }

    /// <summary>
    /// Inserts the words into the given text
    /// </summary>
    /// <param name="words"></param>
    /// <param name="text"></param>
    /// <returns></returns>
    public string setWords(string[] words, string text)
    {
        string output = "";
        int wordIndex = 0;
        for (int i = 0; i < text.Length; i++)
        {
            if (wordDelimiters.Contains(text[i]))
            {
                //Segment delimiter found
                output += words[wordIndex] + text[i];
                wordIndex++;
            }
        }
        output += words[wordIndex];
        return output;
    }
}
