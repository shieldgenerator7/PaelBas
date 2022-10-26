using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject uiElementTemplate;
    public Vector3 startPos;
    public float bufferSpace = 10;

    private List<GameObject> uiElementList = new List<GameObject>();
    private List<string> names = new List<string>();

    private Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponent<Canvas>();
        //Player delegates
        PlayerController pc = FindObjectOfType<PlayerController>();
        pc.obfuscatorPushed += pushObfuscator;
        pc.obfuscatorPopped += popObfuscator;
        pc.messageSwitched += switchMessage;
    }

    public void push(string name)
    {
        names.Add(name);
        refresh();
    }

    public void pop()
    {
        if (names.Count > 0)
        {
            names.RemoveAt(names.Count - 1);
        }
        refresh();
    }

    private void refresh()
    {
        //Destroy old UI components
        foreach (GameObject uiElement in uiElementList)
        {
            Destroy(uiElement);
        }
        uiElementList = new List<GameObject>();
        Vector3 nextpos = startPos;
        //Add new UI components
        foreach (string name in names)
        {
            GameObject newUIElement = Instantiate(uiElementTemplate);
            newUIElement.transform.SetParent(canvas.transform, false);
            TextMeshProUGUI tmpugui = newUIElement.GetComponent<TextMeshProUGUI>();
            tmpugui.text = name;
            uiElementList.Add(newUIElement);
            RectTransform rect = newUIElement.GetComponent<RectTransform>();
            rect.anchoredPosition = nextpos;
            nextpos.y += -bufferSpace;
        }
    }

    public void clear()
    {
        names = new List<string>();
        refresh();
    }

    private void pushObfuscator(Obfuscator obf)
    {
        push(obf.GetType().Name);
    }

    private void popObfuscator()
    {
        pop();
    }

    private void switchMessage(Message m)
    {
        clear();
        //TODO: populate list with names of obfuscators in new message (if any)
    }
}
