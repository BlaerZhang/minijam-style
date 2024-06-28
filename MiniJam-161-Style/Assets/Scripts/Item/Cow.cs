using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cow : MonoBehaviour, IItem
{
    [Header("Follow Settings")]
    public Transform playerTransform;
    public float followSpeed = 10f;
    public float followDistance;

    private bool isFollowing = false;

    void Update()
    {
        if (isFollowing)
        {
            FollowPlayer();
            if (Input.GetKeyDown(KeyCode.F))
            {
                isFollowing = false;
            }
        }

    }

    public void Interact()
    {
        isFollowing = true;
    }

    private void FollowPlayer()
    {
        if (playerTransform != null)
        {
            float distance = Vector3.Distance(transform.position, playerTransform.position);

            if (distance > followDistance)
            {
                Vector3 targetPosition = playerTransform.position;
                transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * followSpeed);
            }
        }
    }
}
