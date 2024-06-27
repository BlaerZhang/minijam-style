using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Goalkeeper : MonoBehaviour, IItemAuto
{
    // public IItem.InteractionTypes interactionTypes = IItem.InteractionTypes.collision;
    // public IItem.InteractionTypes InteractionType
    // {
    //     get => interactionTypes;
    //     set => interactionTypes = value;
    // }

    private GameObject _defendingTarget;

    private bool _isDefending = false;
    private bool _isFailed = false;

    public float moveSpeed = 5f;
    public Vector2 XPosLimits;
    public float detachDistance = 2.5f;

    public void Interact(GameObject gameObject)
    {
        _isDefending = true;
        
        if(_defendingTarget != null)
            if (_defendingTarget.tag == "Player")
                return;
        _defendingTarget = gameObject;
    }

    private void OnEnable()
    {
        Goal.OnGoal += OnGoal;
    }
    
    private void OnDisable()
    {
        Goal.OnGoal -= OnGoal;
    }

    void Update()
    {
        if (_defendingTarget == null) return;
        
        if (_isDefending && !_isFailed)
        {
            Vector3 targetPos = new Vector3(_defendingTarget.transform.position.x, transform.position.y, 0); //TODO: goal orientation based pos
            Vector3 targetPosLimited = new Vector3(Mathf.Clamp(targetPos.x, XPosLimits.x, XPosLimits.y), targetPos.y);
            transform.position += (targetPosLimited - transform.position) * Time.deltaTime * moveSpeed;
        }
        
        if ((_defendingTarget.transform.position - transform.position).magnitude > detachDistance)
        {
            _defendingTarget = null;
            _isDefending = false;
        }
    }

    void OnGoal()
    {
        _isFailed = true;
        //TODO: fail presentation
        GetComponentInChildren<SpriteRenderer>().color = Color.blue;
        
        DOVirtual.DelayedCall(2, () =>
        {
            _isFailed = false;
            //TODO: reset fail presentation
            GetComponentInChildren<SpriteRenderer>().color = Color.white;
        }).Play();
    }
}
