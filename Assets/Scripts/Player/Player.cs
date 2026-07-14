using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class Player : MonoBehaviour
{ 
    public Rigidbody2D mRigidbody;
    public HealthBase _healthBase;

    [Header("Speed Setup")]
    public Vector2 friction = new Vector2(.1f, 0);
    public float speed;
    public float speedRun;
    private float _currentSpeed;
    public float jumpForce = 2;

    [Header("Animation Setup")]
    public float jumpScaley= 1.5f;
    public float jumpScalex = .7f;
    public float animationDuration = 0.3f;
    public Ease ease = Ease.OutBack;

    [Header("Animation player")]
    public string boolrun = "Run";
    public string triggerDeath = "Death";
    public Animator animator;
    public float playerSwipeDuration = .1f;

    

    private void Awake()
    {;
        if (_healthBase != null)
        {
            _healthBase.OnKill += OnPlayerKilled;
        }
    }

    private void OnPlayerKilled()
    {
        _healthBase.OnKill -= OnPlayerKilled;

        animator.SetTrigger(triggerDeath);
    }

    private void Update()
    {
        HandleJump();
        HandleMovement();
        
    }

    private void HandleMovement()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            _currentSpeed = speedRun;
            animator.speed = 2;
        }
        else
        {
            _currentSpeed = speed;
            animator.speed = 1;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            mRigidbody.linearVelocity = new Vector2(-_currentSpeed, mRigidbody.linearVelocity.y);
            if (mRigidbody.transform.localScale.x != -1)
            {
                mRigidbody.transform.DOScaleX(-1, playerSwipeDuration);
            }
            animator.SetBool(boolrun, true);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            mRigidbody.linearVelocity = new Vector2(_currentSpeed, mRigidbody.linearVelocity.y);
            if (mRigidbody.transform.localScale.x != 1)
            {
                mRigidbody.transform.DOScaleX(1, playerSwipeDuration);
            }
            animator.SetBool(boolrun, true);
        }
        else
        {
            animator.SetBool(boolrun, false);
        }

        if (mRigidbody.linearVelocity.x > 0)
        {
            mRigidbody.linearVelocity += friction;
        }
        else if (mRigidbody.linearVelocity.x < 0)
        {
            mRigidbody.linearVelocity -= friction;
        }
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            mRigidbody.linearVelocity = Vector2.up * jumpForce;
            mRigidbody.transform.localScale = Vector2.one;
            DOTween.Kill(mRigidbody.transform);
            HandleScaleJump();
        }
    }

    private void HandleScaleJump()
    {
        mRigidbody.transform.DOScaleY(jumpScaley, animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(ease);
        mRigidbody.transform.DOScaleX(jumpScalex, animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(ease);
    }

    public void DestroyMe()
    {
        Destroy(gameObject);
    }

}
