using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleuthTree
{
    private string startText;
    private SleuthNode root;

    private SleuthNode sleuthNode;

    public string Text => sleuthNode.currentState;

    public SleuthTree(string startText)
    {
        this.startText = startText;
        root = new SleuthNode(startText);
        sleuthNode = root;
    }

    public void saveText(string currentText)
    {
        sleuthNode.saveText(currentText);
    }

    public string pushObfuscator(Obfuscator obf)
    {
        sleuthNode = sleuthNode.addChild(obf);
        return sleuthNode.currentState;
    }

    public string popObfuscator()
    {
        sleuthNode = sleuthNode.parent ?? sleuthNode;
        return sleuthNode.currentState;
    }


}