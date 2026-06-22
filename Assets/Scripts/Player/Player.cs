using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{ 
    public Rigidbody2D mRigidbody;

    public Vector2 friction = new Vector2(.1f, 0);

    public float speed;

    public float jumpForce = 2;

    private void Update()
    {
        HandleJump();
        HandleMovement();
        
    }

    private void HandleMovement()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            mRigidbody.linearVelocity = new Vector2(-speed, mRigidbody.linearVelocity.y);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            mRigidbody.linearVelocity = new Vector2(speed, mRigidbody.linearVelocity.y);
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
        }
    }
}
