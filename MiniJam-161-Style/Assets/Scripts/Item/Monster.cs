using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Monster : MonoBehaviour, IItem
{
    public IItem.InteractionTypes interactionTypes = IItem.InteractionTypes.press;
    public IItem.InteractionTypes InteractionType
    {
        get => interactionTypes;
        set => interactionTypes = value;
    }
    
    private bool isInteractable = true;
    public NightCity nightCity;
    public float stunDuration = 1f;
    public Vector2 travelXStartEnd;
    private Sequence _sequence;
    public void Interact()
    {
        _sequence.Pause();
        isInteractable = false;
        DOVirtual.DelayedCall(stunDuration, () =>
        {
            _sequence.Play();
            isInteractable = true;
        }).Play();
    }

    private void Start()
    {
        _sequence = DOTween.Sequence();
        _sequence
            .Append(transform.DOMoveX(travelXStartEnd.y, 2f))
            .AppendInterval(1)
            .AppendCallback(()=> nightCity.TurnOffLight())
            .Append(transform.DOMoveX(travelXStartEnd.x, 2f))
            .AppendInterval(1)
            .SetLoops(-1, LoopType.Restart);
        _sequence.Play();
    }
}
