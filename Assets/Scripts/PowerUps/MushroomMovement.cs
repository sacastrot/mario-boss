using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MushroomMovement : MonoBehaviour
{
    
    private bool IsGrounded { get; set; }
    public float speed;
    public bool moveLeft;
    private bool born;
    public Animator animator;
    public BoxCollider2D bc;
    private Rigidbody2D rb;
    
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bc.enabled = false;
        born = true;
        moveLeft = RandomBoolean();
    }

    // Update is called once per frame
    void Update()
    {
        if (born)
        {
            animator.SetBool("BornMushroom", born);
            speed = 0;
            StartCoroutine(Move());
            rb.bodyType = RigidbodyType2D.Kinematic;
        }


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
        rb.velocity= Vector2.up * 1;
    }

    private IEnumerator Move()
    {
        yield return new WaitForSeconds(1f);
        born = false;
        animator.SetBool("BornMushroom", born);
        bc.enabled = true;
        speed = 3f;
        rb.bodyType = RigidbodyType2D.Dynamic;
        StopCoroutine(Move());
    }

    private bool RandomBoolean()
    {
        return (Random.value > 0.5f);
    }
}
