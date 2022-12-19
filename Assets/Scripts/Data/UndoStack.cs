using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UndoStack
{
    private List<UndoEntry> undoList = new List<UndoEntry>();

    private int index;
    public int CurrentIndex
    {
        get => index;
        set
        {
            index = Mathf.Clamp(value, 0, undoList.Count - 1);
            onUndoStateChanged?.Invoke(Text);
        }
    }
    public delegate void OnUndoStateChanged(string state);
    public event OnUndoStateChanged onUndoStateChanged;

    public UndoStack(string startText)
    {
        UndoEntry startEntry = new UndoEntry(startText);
        undoList.Add(startEntry);
        CurrentIndex = undoList.Count - 1;
    }

    public void undo()
    {
        if (index > 0)
        {
            CurrentIndex--;
        }
    }

    public void redo()
    {
        CurrentIndex++;
    }

    public void recordState(string text, Obfuscator obfuscator = null)
    {
        //Remove states that would be overwritten
        undoList.RemoveRange(index + 1, undoList.Count - 1 - index);
        //Create new state
        UndoEntry entry = new UndoEntry(text, obfuscator);
        //Add new state
        undoList.Add(entry);
        //Update index
        CurrentIndex = undoList.Count - 1;
    }

    /// <summary>
    /// Reset the stack back to the beginning
    /// </summary>
    public void reset()
    {
        CurrentIndex = 0;
    }

    public string Text => undoList[index].text;

    public List<Obfuscator> ObfuscatorList
        => undoList
        .ConvertAll(entry => entry.obfuscator)
        .FindAll(obf => obf)
        .Distinct().ToList();
}
