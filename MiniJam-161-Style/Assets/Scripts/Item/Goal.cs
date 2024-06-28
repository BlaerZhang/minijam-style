using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public static Action OnGoal;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponentInChildren<SpriteRenderer>().gameObject.CompareTag("Dog"))
        {
            OnGoal?.Invoke();
        }
    }
}
