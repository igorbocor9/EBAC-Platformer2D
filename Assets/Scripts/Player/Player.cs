using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{ 
    public Rigidbody2D mRigidbody;

    public Vector2 velocity;

    public float speed;

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            mRigidbody.linearVelocity = new Vector2(-speed, mRigidbody.linearVelocity.y);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            mRigidbody.linearVelocity = new Vector2(speed, mRigidbody.linearVelocity.y);
        }
    }
}
