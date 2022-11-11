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

    public int LayerCount
    {
        get
        {
            int count = 0;
            SleuthNode node = sleuthNode;
            while (node)
            {
                if (node.Obfuscator)
                {
                    count++;
                }
                node = node.parent;
            }
            return count;
        }
    }

    public List<Obfuscator> ObfuscatorTrail
    {
        get
        {
            List<Obfuscator> obfuscatorTrail = new List<Obfuscator>();
            SleuthNode node = sleuthNode;
            while (node)
            {
                if (node.Obfuscator)
                {
                    obfuscatorTrail.Add(node.Obfuscator);
                }
                node = node.parent;
            }
            obfuscatorTrail.Reverse();
            return obfuscatorTrail;
        }
    }
}
