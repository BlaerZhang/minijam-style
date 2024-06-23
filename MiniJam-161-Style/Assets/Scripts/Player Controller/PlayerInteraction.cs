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

    void OnTriggerEnter2D(Collider2D other)
    {

        IItem item = other.GetComponent<IItem>();
        if (item != null)
        {
            nearbyItem = item;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {

        IItem item = other.GetComponent<IItem>();
        if (item != null && item == nearbyItem)
        {
            nearbyItem = null;
        }
    }
}
