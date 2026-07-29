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

    public GameObject endGameUI;

    private float _currentSpeed;

    //public Animator animator;
    private Animator _currentPlayer;

    [Header("Jump Collision Check")]
    public Collider2D collider2D;
    public float distToGround;
    public float spaceToGround = 0.1f;
    public ParticleSystem jumpVFX;

    private void Awake()
    {;
        if (_healthBase != null)
        {
            _healthBase.OnKill += OnPlayerKilled;
        }

        _currentPlayer = Instantiate(soPlayerSetup.player, transform);

        if (collider2D != null)
        {
            distToGround = collider2D.bounds.extents.y;
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.Raycast(transform.position, -Vector2.up, distToGround + spaceToGround);
    }

    private void OnPlayerKilled()
    {
        _healthBase.OnKill -= OnPlayerKilled;

        _currentPlayer.SetTrigger(soPlayerSetup.triggerDeath);

        CallEndGame();
    }

    public void CallEndGame()
    {
        endGameUI.SetActive(true);
    }

    private void Update()
    {
        IsGrounded();
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
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())  
        {
            mRigidbody.linearVelocity = Vector2.up * soPlayerSetup.jumpForce;
            mRigidbody.transform.localScale = Vector2.one;
            DOTween.Kill(mRigidbody.transform);
            HandleScaleJump();
            PlayJumpVFX();
        }
    }

    private void PlayJumpVFX()
    {
        VFXManager.Instance.PlayVFXByType(VFXManager.VFXType.JUMP, transform.position);
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
