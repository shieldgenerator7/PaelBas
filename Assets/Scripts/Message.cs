using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Message", menuName = "Message")]
public class Message : ScriptableObject
{
    [SerializeField]
    [TextArea(5, 20)]
    private string text;

    public List<Obfuscator> obfuscators;


    public string Text
    {
        get => text;
    }

    public string Untext
    {
        get
        {
            string untext = Text;
            foreach (Obfuscator obf in obfuscators)
            {
                untext = obf.Obfuscate(untext);
            }
            return untext;
        }
    }

    public string UnobfuscatedText
    {
        get
        {
            string unobfuscatedText = Untext;
            for (int i = obfuscators.Count - 1; i >= 0; i--)
            {
                Obfuscator obf = obfuscators[i];
                unobfuscatedText = obf.Unobfuscate(unobfuscatedText);
            }
            return unobfuscatedText;
        }
    }
}
