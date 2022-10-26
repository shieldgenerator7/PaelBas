﻿using System.Collections;
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

    public string CurrentText
    {
        get
        {
            return sleuthNodeCurrent.currentState;
        }
    }

    public List<Obfuscator> obfuscators;

    private SleuthNode sleuthNodeRoot;
    private List<SleuthNode> sleuthNodes = new List<SleuthNode>();
    private SleuthNode sleuthNodeCurrent;

    // Start is called before the first frame update
    public void init()
    {
        generateUntext();

        //SleuthNode Tree
        sleuthNodeRoot = new SleuthNode(Untext);
        sleuthNodeCurrent = sleuthNodeRoot;
    }

    public void generateUntext()
    {
        Untext = Text;
        foreach (Obfuscator obf in obfuscators)
        {
            Untext = obf.obfuscate(Untext);
        }
    }

    public string pushSleuthNode(string currentText, Obfuscator obf)
    {
        sleuthNodeCurrent.saveText(currentText);
        SleuthNode sleuth = sleuthNodeCurrent.addChild(obf);
        if (!sleuthNodes.Contains(sleuth))
        {
            sleuthNodes.Add(sleuth);
        }
        sleuthNodeCurrent = sleuth;
        return sleuthNodeCurrent.currentState;
    }

    public string popSleuthNode(string currentText)
    {
        if (sleuthNodeCurrent != sleuthNodeRoot)
        {
            sleuthNodeCurrent.saveText(currentText);
            sleuthNodeCurrent = sleuthNodeCurrent.parent;
        }
        return sleuthNodeCurrent.currentState;
    }
}
