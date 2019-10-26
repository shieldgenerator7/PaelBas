using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Node that keeps track of the player's progress
/// as they try to unobfuscate the message
/// </summary>
public class SleuthNode
{
    string input;//the text before this node processes
    string output;//the text directly after this node processes
    public string currentState;//the text after further user input

    Obfuscator obfuscator;

    public SleuthNode parent { get; private set; }
    private List<SleuthNode> children;

    public SleuthNode(string input)
    {
        this.input = input;
        this.output = input;
        this.currentState = input;

        this.parent = null;
        this.children = new List<SleuthNode>();
    }

    public SleuthNode(string input, Obfuscator obfuscator)
        : this(input)
    {
        this.obfuscator = obfuscator;
        this.output = this.obfuscator.unobfuscate(this.input);
        this.currentState = this.output;
    }

    public void saveText(string text)
    {
        this.currentState = text;
    }

    public void clearText()
    {
        this.currentState = this.output;
    }

    public SleuthNode addChild(Obfuscator obf)
    {
        SleuthNode foundChild = null;
        foreach(SleuthNode child in children)
        {
            if (child.obfuscator.GetType() == obf.GetType()
                && child.input == currentState) {
                foundChild = child;
                break;
            }
        }
        if (foundChild)
        {
            return foundChild;
        }
        else
        {
            SleuthNode newChild = new SleuthNode(currentState, obf);
            this.link(newChild);
            return newChild;
        }
    }

    /// <summary>
    /// Call like this:   parent.link(newChild);
    /// </summary>
    /// <param name="sleuthNode"></param>
    private void link(SleuthNode sleuthNode)
    {
        this.children.Add(sleuthNode);
        sleuthNode.parent = this;
    }

    public static implicit operator bool (SleuthNode s)
        => s != null;
}
