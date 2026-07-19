using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class Player : MonoBehaviour
{ 
    public Rigidbody2D mRigidbody;
    public HealthBase _healthBase;

    [Header("Setup")]
    public SOPlayerSetup soPlayerSetup;

    private float _currentSpeed;

    //public Animator animator;
    private Animator _currentPlayer;

    private void Awake()
    {;
        if (_healthBase != null)
        {
            _healthBase.OnKill += OnPlayerKilled;
        }

        _currentPlayer = Instantiate(soPlayerSetup.player, transform);
    }

    private void OnPlayerKilled()
    {
        _healthBase.OnKill -= OnPlayerKilled;

        _currentPlayer.SetTrigger(soPlayerSetup.triggerDeath);
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
            _currentSpeed = soPlayerSetup.speedRun;
            _currentPlayer.speed = 2;
        }
        else
        {
            _currentSpeed = soPlayerSetup.speed;
            _currentPlayer.speed = 1;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            mRigidbody.linearVelocity = new Vector2(-_currentSpeed, mRigidbody.linearVelocity.y);
            if (mRigidbody.transform.localScale.x != -1)
            {
                mRigidbody.transform.DOScaleX(-1, soPlayerSetup.playerSwipeDuration);
            }
            _currentPlayer.SetBool(soPlayerSetup.boolrun, true);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            mRigidbody.linearVelocity = new Vector2(_currentSpeed, mRigidbody.linearVelocity.y);
            if (mRigidbody.transform.localScale.x != 1)
            {
                mRigidbody.transform.DOScaleX(1, soPlayerSetup.playerSwipeDuration);
            }
            _currentPlayer.SetBool(soPlayerSetup.boolrun, true);
        }
        else
        {
            _currentPlayer.SetBool(soPlayerSetup.boolrun, false);
        }

        if (mRigidbody.linearVelocity.x > 0)
        {
            mRigidbody.linearVelocity += soPlayerSetup.friction;
        }
        else if (mRigidbody.linearVelocity.x < 0)
        {
            mRigidbody.linearVelocity -= soPlayerSetup.friction;
        }
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            mRigidbody.linearVelocity = Vector2.up * soPlayerSetup.jumpForce;
            mRigidbody.transform.localScale = Vector2.one;
            DOTween.Kill(mRigidbody.transform);
            HandleScaleJump();
        }
    }

    private void HandleScaleJump()
    {
        mRigidbody.transform.DOScaleY(soPlayerSetup.jumpScaleY, soPlayerSetup.animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(soPlayerSetup.ease);
        mRigidbody.transform.DOScaleX(soPlayerSetup.jumpScaleX, soPlayerSetup.animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(soPlayerSetup.ease);
    }

    public void DestroyMe()
    {
        Destroy(gameObject);
    }

}
