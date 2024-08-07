using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public GameObject interactionIndicator;
    // public string tag;
    private IItem nearbyItem;
    private IItemAuto nearbyItemAuto;

    protected virtual void Update()
    {
        // Check for interaction input 
        if (Input.GetKeyDown(KeyCode.F) && nearbyItem != null)
        {
            nearbyItem.Interact();
            if (interactionIndicator != null) interactionIndicator?.SetActive(false);
        }
    }

    //using collider to determine whether an object is interactable
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Interactable")) return;
        //Debug.Log("got in: " + other.name);
        
        IItem item = other.GetComponent<IItem>();
        if (item != null)
        {
            // show interaction indicator
            if (interactionIndicator != null) interactionIndicator?.SetActive(true);
            nearbyItem = item;
        }

        IItemAuto itemAuto = other.GetComponent<IItemAuto>();
        if (itemAuto != null && itemAuto != nearbyItemAuto)
        {
            // Debug.Log("Triggered " + itemAuto);
            nearbyItemAuto = itemAuto;
            
            //TODO: temp interact, to be optimized
            nearbyItemAuto.Interact(this.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Interactable")) return;
        //Debug.Log("got out:" + other.name);
        IItem item = other.GetComponent<IItem>();
        if (item != null && item == nearbyItem)
        {
            if (interactionIndicator != null) interactionIndicator?.SetActive(false);
            nearbyItem = null;
        }
        
        IItemAuto itemAuto = other.GetComponent<IItemAuto>();
        if (itemAuto != null && itemAuto == nearbyItemAuto)
        {
            nearbyItemAuto = null;
        }
    }
}
