using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldIndicator : MonoBehaviour
{
    public Transform holder;
    public PlayerController playerController;

    public Sprite canHoldSprite;
    public Sprite heldSprite;

    private SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        bool show = playerController.Holding || holdObjectFound();
        sr.enabled = show;
        if (show)
        {
            sr.sprite = (playerController.Holding) ? heldSprite : canHoldSprite;
        }
    }

    public bool holdObjectFound()
    {
        //Find object to hold
        RaycastHit[] infos;
        infos = Physics.RaycastAll(
            holder.transform.position,
            holder.transform.forward,
            playerController.maxPickupDistance
            );
        for (int i = 0; i < infos.Length; i++)
        {
            RaycastHit info = infos[i];
            if (info.transform.GetComponent<Interactible>())
            {
                return true;
            }
        }
        return false;
    }
}
