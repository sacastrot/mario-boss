using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Player
    [Header("RigiBody2D")] [SerializeField]
    private Transform _player;
    
    // Movement
    [Header("Player Walk")] [SerializeField] 
    private float _moveSpeedWalk = 1f;
    [Header("Max Speed Walk")] [SerializeField] 
    private float _maxSpeedWalk = 4f;
    [Header("Player Run")] [SerializeField]
    private float _moveSpeedRun = 1f;
    [Header("Max Speed Run")] [SerializeField] 
    private float _maxSpeedRun = 4f;
    
    // Physics
    [Header("Gravity")] [SerializeField] 
    private float _gravity = -9.8f;
    
    // Flags
    public bool walking => Mathf.Abs(_velocity.x) > 0f || Mathf.Abs(input.Move.x) > 0f;
    public bool runing => Mathf.Abs(_velocity.x) > _maxSpeedWalk || Mathf.Abs(input.Move.x) > _maxSpeedWalk;

    public bool turn;
    
    // [Header("Components")] [SerializeField] private Rigidbody2D _rb;
    public PlayerInput input;
    public Vector2 _velocity;
    public bool grounded { get; private set; }
    public bool jumping { get; private set; }

    private Rigidbody2D _rb;
    private float lastHorizontal = 1;

    private float _jumpForce = 6f;

    private bool _hasRB;
    private float _turnTimer;
    
    
    //Ground Check
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;
    private float jumpTimeCounter;

    // Start is called before the first frame update
    void Start()
    {
        input = GetComponent<PlayerInput>();
        
        _hasRB = _player.TryGetComponent(out _rb);
    }

    // Update is called once per frame
    void Update()
    {
        HorizontalMovement(input.Move.x);

        grounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
        //grounded = true;
        if (grounded) {
            GroundedMovement();
        }

        
        ApplyGravity(); 

    }

    private void GroundedMovement()
    {
        // prevent gravity from infinitly building up
        // _velocity.y = Mathf.Max(_velocity.y, 0f);
        // jumping = _velocity.y > 0f;

        // perform jump
        if (Input.GetButtonDown("Jump"))
        {
            _velocity.y = _jumpForce;
            jumping = true;
        }
    }

    private void OnEnable()
    {
        _velocity = Vector2.zero;
    }

    void FixedUpdate()
    {
        Vector2 position = _rb.position;
        position += _velocity * Time.fixedDeltaTime;
        _rb.MovePosition(position);
    }

    private void HorizontalMovement(float moveX)
    {
        _velocity.x = Mathf.MoveTowards(_velocity.x, moveX * _maxSpeedWalk, _moveSpeedWalk * Time.deltaTime);
        
        // flip sprite to face direction
        if (moveX != 0 && Mathf.Abs(_velocity.x)>1f)
        {
            turn = lastHorizontal != moveX;
            lastHorizontal = moveX;
            if (turn)
            {
                _turnTimer = 0.33333f;
            }
        }

        if (_turnTimer > 0)
        {
            _turnTimer -= Time.deltaTime;

        } else if (_velocity.x > 0f)
        {
            transform.eulerAngles = Vector3.zero;
                
        } else if (_velocity.x < 0f)
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
                
        }
    }
    private void ApplyGravity()
    {
        // check if falling
        bool falling = _velocity.y < 0f || !input.Jump;
        float multiplier = falling ? 2f : 1f;

        // apply gravity and terminal velocity
        _velocity.y += _gravity * multiplier * Time.deltaTime;
        _velocity.y = Mathf.Max(_velocity.y, _gravity / 2f);
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Floor");
    }
}