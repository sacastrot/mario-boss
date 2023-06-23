using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class StarMovement : MonoBehaviour
{

    public LayerMask collisionLayers;
    private bool IsGrounded { get; set; }
    public float speed;
    public float jumpForce;
    public bool moveLeft;
    private bool born;
    private Animator animator;
    public BoxCollider2D bc;
    private Rigidbody2D rb;
    
    public PlayerController playerController;
    private float _currentSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        bc.enabled = false;
        born = true;
        moveLeft = RandomBoolean();
        _currentSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (born)
        {
            animator.SetBool("Born", born);
            _currentSpeed = 0;
            StartCoroutine(Move());
            rb.bodyType = RigidbodyType2D.Kinematic;
        }


        IsGrounded = Physics2D.Raycast(transform.position, Vector2.down, 0.4f);
        if (moveLeft)
        {
            transform.Translate(-2* Time.deltaTime* _currentSpeed, 0, 0);
        }
        else
        {
            transform.Translate(2* Time.deltaTime* _currentSpeed, 0, 0);
        }

        if (IsGrounded)
        {
            starJump();
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if ((collisionLayers.value & 1<<col.gameObject.layer) == 1<<col.gameObject.layer)
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

        if (col.collider.gameObject.layer == 8)
        {
            playerController.invensible = true;
            Destroy(gameObject);
        }
    }

    private void starJump()
    {
        rb.velocity= Vector2.up * jumpForce;
    }

    private IEnumerator Move()
    {
        yield return new WaitForSeconds(1f);
        born = false;
        animator.SetBool("Born", born);
        bc.enabled = true;
        _currentSpeed = speed;
        rb.bodyType = RigidbodyType2D.Dynamic;
        StopCoroutine(Move());
    }
    
    private bool RandomBoolean()
    {
        return (Random.value > 0.5f);
    }
}
