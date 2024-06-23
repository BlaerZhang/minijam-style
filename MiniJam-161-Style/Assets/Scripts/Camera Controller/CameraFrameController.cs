using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraFrameController : MonoBehaviour
{
    public float changeScaleDuration = 0.1f;
    public float followSpeed = 10000;
    public GameObject photographyCamera;
    
    public void UpdateSize(float scale)
    {
        transform.DOScale(scale, changeScaleDuration);
    }

    private void Update()
    {
        // follow actual camera
        transform.position += (photographyCamera.transform.position - transform.position) * followSpeed;
    }
}
