using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player")] [SerializeField] private float _moveSpeed = 1f;

    [Header("Player Run")] [SerializeField]
    private float _moveSpeedRun = 1f;

    [Header("Animator")] [SerializeField] private Transform _body;

    [Header("RigiBody2D")] [SerializeField]
    private Transform _player;

    [Header("Gravity")] [SerializeField] private float _gravity = -9.8f;
    [Header("Speed")] [SerializeField] private float _maxSpeed = 4f;
    [Header("Speed")] [SerializeField] private float _maxSpeedRun = 4f;
    [Header("Speed")] [SerializeField] private float _maxSpeedWalk = 4f;

    // [Header("Components")] [SerializeField] private Rigidbody2D _rb;
    private PlayerInput _input;
    private Animator _anim;

    private Rigidbody2D _rb;
    private bool _hasAnimator;

    private bool _hasRB;

    // Start is called before the first frame update
    void Start()
    {
        _input = GetComponent<PlayerInput>();
        _hasAnimator = _body.TryGetComponent(out _anim);
        _hasRB = _player.TryGetComponent(out _rb);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {
        Walk(_input.Move.x);
        Look(_input.Move.y);
        Run(_input.Move.x);
        modifyPhysics();
    }

    private void Look(float moveY)
    {
        Debug.Log(moveY);
        _anim.SetFloat("AnimLookY", moveY);
    }

    private void Walk(float moveX)
    {
        if (Mathf.Abs(_rb.velocity.x) < _maxSpeedWalk)
        {
            _rb.AddForce(Vector2.right * moveX * _moveSpeed);
        }

        _anim.SetFloat("AnimMoveX", _rb.velocity.x);
    }

    private void modifyPhysics()
    {
        if (Mathf.Abs(_input.Move.x) < 0.4f)
        {
            _rb.drag = 4f;
        }
        else
        {
            _rb.drag = 0;
        }
    }

    private void Run(float moveX)
    {
        if (Mathf.Abs(_rb.velocity.x) >= _maxSpeedRun)
        {
            _anim.speed = 2.5f;

            if (_input.keyZ)
            {
                Debug.Log("Key Down Z");
                if (Mathf.Abs(_rb.velocity.x) < _maxSpeedRun + 4)
                {
                    _rb.AddForce(Vector2.right * moveX * _moveSpeedRun);
                }
            }

            _anim.SetBool("isRun", _input.keyZ);
        }
        else
        {
            _anim.speed = 1f;
        }

        if (Mathf.Abs(_rb.velocity.x) < _maxSpeedRun)
        {
            _rb.AddForce(Vector2.right * moveX * _moveSpeed);
        }

        _anim.SetFloat("AnimMoveX", _rb.velocity.x);
    }
}