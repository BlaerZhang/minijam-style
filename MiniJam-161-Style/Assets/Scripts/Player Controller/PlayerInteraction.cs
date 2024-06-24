using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private IItem nearbyItem;

    void Update()
    {
        // Check for interaction input 
        if (Input.GetKeyDown(KeyCode.F) && nearbyItem != null)
        {
            nearbyItem.Interact();
        }
    }

    //using collider to determine whether an object is interactable
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("got in");

        IItem item = other.GetComponent<IItem>();
        if (item != null)
        {
            nearbyItem = item;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("got out");
        IItem item = other.GetComponent<IItem>();
        if (item != null && item == nearbyItem)
        {
            nearbyItem = null;
        }
    }
}
