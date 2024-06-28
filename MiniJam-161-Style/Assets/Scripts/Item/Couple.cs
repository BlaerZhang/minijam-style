using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class Couple : MonoBehaviour
{
    [Header("Object Detection")]
    public float detectionRadius = 2f;

    [Header("Romantic Banner Settings")]
    public SpriteRenderer romanticBanner;
    public float fadeDuration = 1f;
    public float floatDistance = 0.1f;
    public float floatDuration = 1f;

    private CircleCollider2D _collider2D;

    private bool isRomantic = false;
    private bool isKidAround = false;
    private bool isFireworkAround = false;
    // TODO: check the weather condition
    private bool isRainingOrRainbow = false;

    void Start()
    {
        _collider2D = GetComponent<CircleCollider2D>();
        _collider2D.radius = detectionRadius;

        RomanticBannerFloating();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        DetectRomantics();
    }

    void OnTriggerExit2D(Collider2D other)
    {
        DetectRomantics();
    }

    private void DetectRomantics()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius);

        isKidAround = false;
        isFireworkAround = false;

        foreach (Collider2D col in colliders)
        {
            if (col.tag.Equals("Kid"))
            {
                isKidAround = true;
                break;
            }
            if (col.tag.Equals("Firework"))
            {
                isFireworkAround = true;
            }
            // Debug.Log("Detected object: " + col.name);
        }

        if (!isKidAround & isFireworkAround & isRainingOrRainbow)
        // if (!isKidAround & isFireworkAround)
        {
            if (!isRomantic)
            {
                isRomantic = true;
                ShowRomanticBanner();
            }
        }
        else
        {
            if (isRomantic)
            {
                HideRomanticBanner();
                isRomantic = false;
            }
        }
    }

    private void ShowRomanticBanner()
    {
        romanticBanner.gameObject.SetActive(true);
        romanticBanner.DOFade(1, fadeDuration);
    }

    private void HideRomanticBanner()
    {
        romanticBanner.DOFade(0, fadeDuration).OnComplete(() => { romanticBanner.gameObject.SetActive(false); });
    }

    void RomanticBannerFloating()
    {
        Vector3 initialPosition = romanticBanner.transform.position;

        romanticBanner.transform.DOMoveY(initialPosition.y + floatDistance, floatDuration)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo)
            .OnUpdate(() =>
            {
                if (romanticBanner.transform.position.y > initialPosition.y + floatDistance)
                {
                    romanticBanner.transform.position = new Vector3(romanticBanner.transform.position.x, initialPosition.y + floatDistance, romanticBanner.transform.position.z);
                }
                else if (romanticBanner.transform.position.y < initialPosition.y)
                {
                    romanticBanner.transform.position = new Vector3(romanticBanner.transform.position.x, initialPosition.y, romanticBanner.transform.position.z);
                }
            });
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, detectionRadius);
    }
}
