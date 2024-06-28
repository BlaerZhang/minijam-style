using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Dog : MonoBehaviour, IItem
{
    public float idleSpeed = 2;
    public float rollingSpeed = 4;
    public Vector2 respawnPos;
    
    private Vector2 startPos;
    private Transform player;
    private Vector2 currentRollingDir;
    
    
    private bool _isRolling = false;
    private bool _isBouncingBack = false;
    
    public void Interact()
    {
        if (_isRolling) return;
        transform.DOKill();
        currentRollingDir = (transform.position - player.position).normalized;
        _isRolling = true;
    }

    private void OnEnable()
    {
        Goal.OnGoal += OnGoal;
    }
    
    private void OnDisable()
    {
        Goal.OnGoal -= OnGoal;
    }

    void Start()
    {
        startPos = transform.position;
        player = FindObjectOfType<PlayerMovement>().transform;
        Patrol();
    }
    
    void Update()
    {
        Roll();
    }

    void Roll()
    {
        if (_isRolling)
        {
            if (_isBouncingBack)
            {
                currentRollingDir = ((Vector3)startPos - transform.position).normalized;
                if (((Vector2)transform.position - startPos).magnitude < 0.05f)
                {
                    _isBouncingBack = false;
                    _isRolling = false;
                    Patrol();
                }
            }
            //rolling
            transform.position += (Vector3)currentRollingDir * rollingSpeed * Time.deltaTime;
            
            //respawn if out
            Vector2 posOnScreen = Camera.main.WorldToScreenPoint(transform.position);
            if (posOnScreen.x <= 0 || posOnScreen.x >= Screen.width || posOnScreen.y <= 0 ||
                posOnScreen.y >= Screen.height) Respawn();
            
            //detect any collision
            if (!_isBouncingBack)
            {
                Collider2D[] collisionResult = new Collider2D[1];
                if (Physics2D.OverlapBoxNonAlloc(transform.position, new Vector2(1, 0.2f), 0, collisionResult) >= 1)
                {
                    if(!collisionResult[0].isTrigger) _isBouncingBack = true;
                }
            }
        }
    }

    void Patrol()
    {
        transform.DOMove(startPos + Vector2.right * 1, 1 / idleSpeed).SetLoops(-1, LoopType.Yoyo);
    }
    
    void OnGoal()
    {
        _isRolling = false;
        DOVirtual.DelayedCall(2, () =>
        {
            ResetIdle();
        }).Play();
    }

    void ResetIdle()
    {
        transform.DOMove(startPos, ((Vector2)transform.position - startPos).magnitude / idleSpeed).OnComplete(() =>
        {
            Patrol();
        });
    }

    void Respawn()
    {
        transform.position = respawnPos;
        transform.DOMove(startPos, ((Vector2)transform.position - startPos).magnitude / rollingSpeed)
            .OnComplete(() =>
            {
                _isRolling = false;
                Patrol();
            });
    }

    // private void OnCollisionEnter2D(Collision2D other)
    // {
    //     print(other.gameObject.name);
    //     if (_isRolling && !_isBouncingBack)
    //     {
    //         _isBouncingBack = true;
    //     }
    // }
}
