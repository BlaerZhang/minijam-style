using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Forest : MonoBehaviour, IItem
{
    // public IItem.InteractionTypes interactionTypes = IItem.InteractionTypes.collision;
    // public IItem.InteractionTypes InteractionType
    // {
    //     get => interactionTypes;
    //     set => interactionTypes = value;
    // }
    
    private bool isInteractable = true;
    public GameObject bird;
    public void Interact()
    {
        Instantiate(bird, transform.position, Quaternion.identity);
    }

    // private void OnCollisionEnter2D(Collision2D other)
    // {
    //     print(other.collider.name);
    //     if (!other.collider.CompareTag("Player")) return;
    //     isInteractable = false;
    //     Instantiate(bird, transform.position, Quaternion.identity);
    // }
    //
    // private void OnCollisionExit2D(Collision2D other)
    // {
    //     print(other.collider.name);
    //     if(!other.collider.CompareTag("Player")) return;
    //     isInteractable = true;
    // }
    //
    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     if(!other.CompareTag("Player")) return;
    //     isInteractable = false;
    //     Instantiate(bird, transform.position, Quaternion.identity);
    // }
    //
    // private void OnTriggerExit2D(Collider2D other)
    // {
    //     if(!other.CompareTag("Player")) return;
    //     isInteractable = true;
    // }
}
