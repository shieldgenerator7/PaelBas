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
                string swapWord = words[swapIndex];
                (word, swapWord) = swapFirstCharacter(word, swapWord);
                words[iW] = word;
                words[swapIndex] = swapWord;
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

    private (string, string) swapFirstCharacter(string word1, string word2)
    {
        //Get the first letter
        char char1 = word1[0];
        char char2 = word2[0];
        //Swap case
        (char1, char2) = Utility.swapCase(char1, char2);
        //Switch letters of words
        string newWord1 = char2 + word1.Substring(1);
        string newWord2 = char1 + word2.Substring(1);
        return (newWord1, newWord2);
    }
}
