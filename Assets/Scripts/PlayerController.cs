using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [TextArea]
    public string currentText;

    public Obfuscator nextFilter;
    public bool pop;

    private Message message;
    
    // Start is called before the first frame update
    void Start()
    {
        //string[] result = "...a.b. c.d.".Split('.', ' ');
        //Debug.Log("result: " + result.Length + " " + result);
        //for (int i = 0; i < result.Length; i++)
        //{
        //    Debug.Log("r[i]: " + result[i]
        //        + " ==" + (result[i] == "")
        //        );
        //}
        message = GetComponent<Message>();
        currentText = message.Untext;
    }

    // Update is called once per frame
    void Update()
    {
        if (nextFilter != null)
        {
            currentText = message.pushSleuthNode(currentText, nextFilter);
            nextFilter = null;
        }
        if (pop)
        {
            currentText = message.popSleuthNode(currentText);
            pop = false;
        }
    }
}
