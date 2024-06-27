using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteractionTest : MonoBehaviour, IItem
{
    public void Interact()
    {
        Debug.Log("Interacted with " + gameObject.name);
        // Add interaction logic here
    }
}

