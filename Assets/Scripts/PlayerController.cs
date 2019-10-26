using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public string input = "";
    public bool obfuscate = true;
    public List<string> output = new List<string>();

    private string prevInput = "";

    public List<Obfuscator> obfuscators;

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

    }

    // Update is called once per frame
    void Update()
    {
        if (input != prevInput)
        {
            prevInput = input;
            string text = input;
            foreach (Obfuscator obf in obfuscators)
            {
                text = (obfuscate)
                    ? obf.obfuscate(text)
                    : obf.unobfuscate(text);
            }
            output.Add(text);
        }
    }
}
