using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
/// <summary>
/// Marks this object as something that the player can interact with
/// </summary>
public class Interactible : MonoBehaviour
{
    public bool holdable = false;
    public bool notable = false;

    public bool Noted
        => MessagePuzzleManager.instance.hasMessage(
            GetComponent<MessageChecker>().message
            );
}
