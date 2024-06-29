using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFO : MonoBehaviour
{
    public Transform objectToFollow;
    public float followSpeed = 10f;
    public float followDistance = 2f;

    void Update()
    {
        Follow();
    }

    private void Follow()
    {
        if (objectToFollow != null)
        {
            Vector2 currentPosition = transform.position;
            currentPosition.x = objectToFollow.position.x;
            float targetPositionY = objectToFollow.position.y + followDistance;
            currentPosition.y = Mathf.Lerp(transform.position.y, targetPositionY, Time.deltaTime * followSpeed);
            transform.position = currentPosition;
        }
    }
}
