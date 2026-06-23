using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class Player : MonoBehaviour
{ 
    public Rigidbody2D mRigidbody;

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
    public bool isJumping = false;

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
        }
        else
        {
            _currentSpeed = speed;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            mRigidbody.linearVelocity = new Vector2(-_currentSpeed, mRigidbody.linearVelocity.y);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            mRigidbody.linearVelocity = new Vector2(_currentSpeed, mRigidbody.linearVelocity.y);
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
            isJumping = true;
            mRigidbody.linearVelocity = Vector2.up * jumpForce;
            mRigidbody.transform.LocalScale = Vector2.one;
            DOTween.Kill(mRigidbody.transform);
            HandleScaleJump();
        }
    }

    private void HandleScaleJump()
    {
        mRigidbody.transform.DOScaleY(jumpScaley, animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(ease);
        mRigidbody.transform.DOScaleX(jumpScalex, animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(ease);
    }

}
