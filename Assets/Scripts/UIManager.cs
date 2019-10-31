using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject uiElementTemplate;

    private List<GameObject> uiElementList = new List<GameObject>();

    private Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponent<Canvas>();
    }

    public void push(string name)
    {
        GameObject newUIElement = Instantiate(uiElementTemplate);
        newUIElement.transform.SetParent(canvas.transform, false);
        TextMeshProUGUI tmpugui = newUIElement.GetComponent<TextMeshProUGUI>();
        tmpugui.text = name;
        uiElementList.Add(newUIElement);
    }

    public void pop()
    {
        uiElementList.RemoveAt(uiElementList.Count - 1);
    }
}
