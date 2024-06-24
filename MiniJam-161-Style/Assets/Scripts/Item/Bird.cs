using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public float flyDuration = 2f;
    public Vector2 targetOffset;
    
    void Start()
    {
        transform.DOLocalMove((Vector2)transform.position + targetOffset, flyDuration)
            .OnComplete(() => Destroy(gameObject));
    }
}
