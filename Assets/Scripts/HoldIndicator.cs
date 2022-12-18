using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldIndicator : MonoBehaviour
{
    public Transform holder;
    public PlayerController playerController;

    public Sprite canHoldSprite;
    public Sprite heldSprite;
    public Sprite noteSprite;

    private SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Interactible interactible = holdObjectFound();
        bool show = playerController.Holding || interactible;
        sr.enabled = show;
        if (show)
        {
            sr.sprite = (interactible.notable)
                ? noteSprite
                : (playerController.Holding) ? heldSprite : canHoldSprite;
        }
    }

    public Interactible holdObjectFound()
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
            Interactible interactible = info.transform.GetComponent<Interactible>();
            if (interactible)
            {
                return interactible;
            }
        }
        return null;
    }
}
