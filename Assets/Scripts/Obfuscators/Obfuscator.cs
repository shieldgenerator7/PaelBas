using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Obfuscator : MonoBehaviour
{
    public static List<char> segmentDelimiters = new List<char>()
    {
        '.',
        '\n',
        '\r'
    };
    public static List<char> wordDelimiters = new List<char>()
    {
        '.',
        '\n',
        '\r',
        ',',
        ' '
    };

    public abstract string obfuscate(string text);

    public abstract string unobfuscate(string untext);

    public string[] getSegments(string text)
    {
        return text.Split(segmentDelimiters.ToArray());
    }
    
    public string[] getWords(string text)
    {
        return text.Split(' ', ',');
    }

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
        for(int i = 0; i < text.Length; i++)
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
