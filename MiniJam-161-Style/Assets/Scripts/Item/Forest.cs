using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Forest : MonoBehaviour, IItemAuto
{
    private bool isInteractable = true;
    public GameObject bird;
    public void Interact(GameObject gameObject)
    {
        // Debug.Log("bird");
        Instantiate(bird, transform.position, Quaternion.identity);
    }
}
