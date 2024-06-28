using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItemAuto
{
    // enum InteractionTypes
    // {
    //     press,
    //     collision
    // }
    //
    // InteractionTypes InteractionType { get; set; }

    void Interact(GameObject interactorGameObject);
}