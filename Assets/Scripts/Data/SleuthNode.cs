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

    public Obfuscator Obfuscator { get; private set; }

    public SleuthNode parent { get; private set; }
    private Dictionary<Obfuscator, SleuthNode> children;

    public SleuthNode(string input)
    {
        this.input = input;
        this.output = input;
        this.currentState = input;

        this.parent = null;
        this.children = new Dictionary<Obfuscator, SleuthNode>();
    }

    public SleuthNode(string input, Obfuscator obfuscator)
        : this(input)
    {
        this.Obfuscator = obfuscator;
        this.output = obfuscator.Unobfuscate(this.input);
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
        //If child exists for the given obfuscator,
        if (children.ContainsKey(obf))
        {
            //And nothing has changed at this node since it was added,
            SleuthNode foundChild = children[obf];
            if (currentState == foundChild.input)
            {
                //Return the existing child
                return foundChild;
            }
        }
        //Else create a new child
        //(if one doesnt already exist, or one exists but is outdated)
        SleuthNode newChild = new SleuthNode(currentState, obf);
        this.link(obf, newChild);
        return newChild;
    }

    /// <summary>
    /// Call like this:   parent.link(newChild);
    /// </summary>
    /// <param name="sleuthNode"></param>
    private void link(Obfuscator obf, SleuthNode sleuthNode)
    {
        //Remove old one if it exists
        if (this.children.ContainsKey(obf))
        {
            this.children.Remove(obf);
        }
        //Add this one
        this.children.Add(obf, sleuthNode);
        sleuthNode.parent = this;
    }

    public static implicit operator bool(SleuthNode s)
        => s != null;
}
