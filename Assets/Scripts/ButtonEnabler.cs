using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonEnabler : MonoBehaviour
{
    public GameObject button;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            button.SetActive(true);
        }
    }
}
