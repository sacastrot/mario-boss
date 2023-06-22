using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star_Movement : MonoBehaviour
{
    
    private bool IsGrounded { get; set; }
    public float speed;
    public float jumpForce;
    public bool moveLeft;
    
    private Rigidbody2D rb;
    
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        IsGrounded = Physics2D.Raycast(transform.position, Vector2.down, 0.4f);
        if (moveLeft)
        {
            transform.Translate(-2* Time.deltaTime* speed, 0, 0);
        }
        else
        {
            transform.Translate(2* Time.deltaTime* speed, 0, 0);
        }

        if (IsGrounded)
        {
            starJump();
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.gameObject.layer==8)
        {
            if (moveLeft)
            {
                moveLeft = false;
            }
            else
            {
                moveLeft = true;
            }
        }

        if (col.collider.gameObject.layer == 30)
        {
            print("dakdajdlakdjadlakjd");
            Destroy(gameObject);
        }
    }

    private void starJump()
    {
        rb.velocity= Vector2.up * jumpForce;
    }
}
