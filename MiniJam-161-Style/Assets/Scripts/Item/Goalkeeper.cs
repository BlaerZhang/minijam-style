using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goalkeeper : MonoBehaviour, IItem
{
    public IItem.InteractionTypes interactionTypes = IItem.InteractionTypes.collision;
    public IItem.InteractionTypes InteractionType
    {
        get => interactionTypes;
        set => interactionTypes = value;
    }

    private bool _isDefending = false;

    public void Interact()
    {
        _isDefending = true;
    }
    
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        
    }
}
