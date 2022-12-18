using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoldIndicator : MonoBehaviour
{
    public Transform holder;
    public PlayerController playerController;

    public Sprite canHoldSprite;
    public Sprite heldSprite;
    public Sprite noteSprite;

    private SpriteRenderer sr;
    private Image image;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        Interactible interactible = holdObjectFound();
        bool show = !playerController.Notebooking && 
            (playerController.Holding || interactible);
        Sprite sprite = null;
        if (show)
        {
            sprite = (interactible.notable)
                ? noteSprite
                : (playerController.Holding) ? heldSprite : canHoldSprite;
        }
        if (sr)
        {
            sr.enabled = show; 
            sr.sprite = sprite;
        }
        if (image)
        {
            image.enabled = show;
            image.sprite = sprite;
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
