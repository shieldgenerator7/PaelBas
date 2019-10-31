﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Message : MonoBehaviour
{
    [SerializeField]
    [TextArea]
    private string text;
    public string Text
    {
        get => text;
        private set => text = value;
    }

    [SerializeField]
    [TextArea]
    private string untext;
    public string Untext
    {
        get => untext;
        private set => untext = value;
    }

    public string CurrentText
    {
        get
        {
            return sleuthNodeCurrent.currentState;
        }
    }
    
    public List<Obfuscator> obfuscators;

    public UIManager uiManager;

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

    public string pushSleuthNode(string currentText, Obfuscator obf)
    {
        sleuthNodeCurrent.saveText(currentText);
        SleuthNode sleuth = sleuthNodeCurrent.addChild(obf);
        if (!sleuthNodes.Contains(sleuth))
        {
            sleuthNodes.Add(sleuth);
        }
        sleuthNodeCurrent = sleuth;
        uiManager.push(obf.GetType().Name);
        return sleuthNodeCurrent.currentState;
    }

    public string popSleuthNode(string currentText)
    {
        sleuthNodeCurrent.saveText(currentText);
        sleuthNodeCurrent = sleuthNodeCurrent.parent;
        uiManager.pop();
        return sleuthNodeCurrent.currentState;
    }
}
