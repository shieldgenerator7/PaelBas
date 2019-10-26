using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Message : MonoBehaviour
{
    [SerializeField]
    private string text;
    public string Text
    {
        get => text;
        private set => text = value;
    }

    [SerializeField]
    private string untext;
    public string Untext
    {
        get => untext;
        private set => untext = value;
    }
    
    public List<Obfuscator> obfuscators;

    private SleuthNode sleuthNodeRoot;
    private List<SleuthNode> sleuthNodes = new List<SleuthNode>();
    private SleuthNode sleuthNodeCurrent;

    // Start is called before the first frame update
    void Start()
    {
        Untext = Text;
        foreach (Obfuscator obf in obfuscators)
        {
            Untext = obf.obfuscate(Untext);
        }

        //SleuthNode Tree
        sleuthNodeRoot = new SleuthNode(Untext);
        sleuthNodeCurrent = sleuthNodeRoot;
    }

    public void pushSleuthNode(string currentText, Obfuscator obf)
    {
        sleuthNodeCurrent.saveText(currentText);
        SleuthNode sleuth = sleuthNodeCurrent.addChild(obf);
        if (!sleuthNodes.Contains(sleuth))
        {
            sleuthNodes.Add(sleuth);
        }
    }

    public string popSleuthNode(string currentText)
    {
        sleuthNodeCurrent.saveText(currentText);
        sleuthNodeCurrent = sleuthNodeCurrent.parent;
        return sleuthNodeCurrent.currentState;
    }
}
