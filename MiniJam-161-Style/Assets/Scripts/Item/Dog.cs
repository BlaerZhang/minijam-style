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
    public float bouncingBackInitialInvincibleTime = 0.5f;
    public float outOfScreenOffset = 0.5f;
    
    private Vector2 startPos;
    private Transform player;
    private Vector2 currentRollingDir;
    private float _invincibleTimer = 0f;
    private Vector2 _currentSpeed;
    private Vector2 lastPos;

    private bool _isRolling = false;
    private bool _isBouncingBack = false;
    private bool _isOut = false;

    public void Interact()
    {
        if (_isRolling) return;
        transform.DOKill();
        currentRollingDir = (transform.position - player.position).normalized;
        _invincibleTimer = bouncingBackInitialInvincibleTime;
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
        lastPos = startPos;
        player = FindObjectOfType<PlayerMovement>().transform;
        Patrol();
    }
    
    void Update()
    {
        Roll();
    }

    private void FixedUpdate()
    {
        _currentSpeed = ((Vector2)transform.position - lastPos) / Time.deltaTime;
        lastPos = transform.position;
    }

    void Roll()
    {
        if (_isRolling)
        {
            //bouncing back
            if (_isBouncingBack)
            {
                currentRollingDir = ((Vector3)startPos - transform.position).normalized;
                if (((Vector2)transform.position - startPos).magnitude < 0.05f)
                {
                    _isBouncingBack = false;
                    _isRolling = false;
                    _isOut = false;
                    Patrol();
                }
            }
            //rolling
            transform.position += (Vector3)currentRollingDir * rollingSpeed * Time.deltaTime;
            
            //respawn if out
            if (!_isOut)
            {
                Vector2 posOnScreen = Camera.main.WorldToScreenPoint(transform.position);
                if (posOnScreen.x <= 0 - outOfScreenOffset ||
                    posOnScreen.x >= Screen.width + outOfScreenOffset ||
                    posOnScreen.y <= 0 - outOfScreenOffset ||
                    posOnScreen.y >= Screen.height + outOfScreenOffset) Respawn();
            }
           
            
            //detect any collision
            if (!_isBouncingBack)
            {
                _invincibleTimer -= Time.deltaTime;
                if (_invincibleTimer >= 0) return;
                
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
        Sequence patrolSequence = DOTween.Sequence();
        patrolSequence
            .Append(transform.DOMoveY(startPos.y + 0.5f, 1f / idleSpeed))
            .Append(transform.DOMoveY(startPos.y, 1f / idleSpeed))
            .Append(transform.DOMoveY(startPos.y - 0.5f, 1f / idleSpeed))
            .Append(transform.DOMoveY(startPos.y, 1f / idleSpeed)).SetEase(Ease.Linear).SetTarget(transform);

        patrolSequence.SetLoops(-1, LoopType.Restart).Play();
        transform.DOMoveX(startPos.x + 2, 2 / idleSpeed).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
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
        transform.DOMove(startPos, ((Vector2)transform.position - startPos).magnitude / idleSpeed).OnComplete(Patrol);
    }

    void Respawn()
    {
        _isOut = true;
        transform.position = respawnPos;
        _isBouncingBack = true;
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
