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
    }

    private void Update()
    {
        isRainingOrRainbow = Elephant.deviceTriggered;
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

        // if (!isKidAround & isFireworkAround & isRainingOrRainbow)
        if (!isKidAround & isFireworkAround & isRainingOrRainbow)
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
        romanticBanner.DOFade(0, 0.1f).OnComplete(() => { romanticBanner.gameObject.SetActive(false); });
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, detectionRadius);
    }
}
