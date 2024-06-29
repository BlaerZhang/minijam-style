using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Monster : MonoBehaviour, IItem
{
    private bool isInteractable = true;
    public NightCity nightCity;
    public GameObject fire;
    public float stunDuration = 1f;
    public float nightCityPosXOffset;
    public float outOfScreenOffset = 50;
    private Sequence _sequence;
    public void Interact()
    {
        // _sequence.Pause();
        // isInteractable = false;
        // DOVirtual.DelayedCall(stunDuration, () =>
        // {
        //     _sequence.Play();
        //     isInteractable = true;
        // }).Play();
    }

    private void Start()
    {
        Vector2 startPos =
            new Vector2(Camera.main.ScreenToWorldPoint(new Vector3(0 - outOfScreenOffset, 0)).x, transform.position.y);
        
        Vector2 endPos =
            new Vector2(Camera.main.ScreenToWorldPoint(new Vector3(Screen.width + outOfScreenOffset, 0)).x, transform.position.y);

        transform.position = startPos;
        fire.SetActive(false);

        _sequence = DOTween.Sequence();
        _sequence
            .Append(transform.DOMoveX(nightCity.transform.position.x - nightCityPosXOffset, 5f).SetEase(Ease.Linear))
            .AppendInterval(0)
            .AppendCallback(() => 
            {
                fire.SetActive(true);
                nightCity.TurnOffLight();
            })
            .Append(transform.DOMoveX(endPos.x, 2f).SetEase(Ease.Linear))
            .AppendCallback(() =>
            {
                transform.position = startPos;
                fire.SetActive(false);
            })
            .SetLoops(-1, LoopType.Restart);
        
        _sequence.Play();
    }
}
