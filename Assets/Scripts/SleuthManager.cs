using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleuthManager : MonoBehaviour
{
    public static SleuthManager instance;

    
    private SleuthTree sleuthTree;

    private void Awake()
    {
        instance = this;

        MessageManager.instance.onMessageSwitched += setSleuthTree;
    }

    public void setSleuthTree(Message message)
    {
        sleuthTree = getTree(message);
    }


    public void pushObfuscator(Obfuscator obf)
    {
        sleuthTree.pushObfuscator(obf);
        onObfuscatorPushed?.Invoke(obf);
    }
    public delegate void ObfuscatorPushed(Obfuscator obf);
    public ObfuscatorPushed onObfuscatorPushed;

    public void popObfuscator()
    {
        sleuthTree.popObfuscator();
        onObfuscatorPopped?.Invoke();
    }
    public delegate void ObfuscatorPopped();
    public ObfuscatorPopped onObfuscatorPopped;

    public void saveText(string text)
    {
        sleuthTree.saveText(text);
    }
}
