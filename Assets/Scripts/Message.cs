using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Message", menuName = "Message")]
public class Message : ScriptableObject
{
    [SerializeField]
    [TextArea(5, 20)]
    private string text;
    public string Text
    {
        get => text;
        private set => text = value;
    }

    public string Untext { get; private set; }


    public List<Obfuscator> obfuscators;

    public SleuthTree SleuthTree { get; private set; }

    // Start is called before the first frame update
    public void init()
    {
        generateUntext();

        //SleuthNode Tree
        SleuthTree = new SleuthTree(Untext);
    }

    public string generateUntext()
    {
        Untext = Text;
        foreach (Obfuscator obf in obfuscators)
        {
            Untext = obf.obfuscate(Untext);
        }
        return Untext;
    }

    public string getUnobfuscatedText()
    {
        string untext = Untext;
        for (int i = obfuscators.Count - 1; i >= 0; i--)
        {
            Obfuscator obf = obfuscators[i];
            untext = obf.unobfuscate(untext);
        }
        return untext;
    }
}
