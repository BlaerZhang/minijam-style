using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraFrameController : MonoBehaviour
{
    public float changeScaleDuration = 0.1f;
    [Range(0,1)] public float followSpeed = 0.05f;
    public GameObject photographyCamera;

    private RectTransform _rectTransform;

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void UpdateSize(Vector2 sizeDelta)
    {
        _rectTransform.DOSizeDelta(sizeDelta, changeScaleDuration);
    }

    private void Update()
    {
        // follow actual camera
        transform.position += (photographyCamera.transform.position - transform.position) * followSpeed;
    }
}
