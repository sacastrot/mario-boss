using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FiniteStateMachine : MonoBehaviour
{
    [Space(10)]
    [SerializeField] private Animator _anim;
    
    [Space(10)]
    [SerializeField] private Transform _target;

    private int _layerCollision;
    private int _currentLayerCollision;

    
    
    [Space(10)]
    [SerializeField] public Rigidbody2D rb;
    
    [Space(10)]
    [SerializeField] public Transform enemy;

    public Transform Target => _target;
    public EnemyConfig Config => _config;
    public int CurrentLayerCollision => _currentLayerCollision;
    
    private bool _hasRb;
    private EnemyConfig _config;
    private Dictionary<StateType, State> _statesDic = new();
    private StateType _currentState;
    
    void Start()
    {
        _config = GetComponent<EnemyConfig>();

        _hasRb = TryGetComponent<Rigidbody2D>(out rb);
        
        Bind(_config.FSMData);

        ToState(_config.InitialState);
    }
    
    void Update()
    {
        if (_statesDic.ContainsKey(_currentState))
        {
            _statesDic[_currentState].OnUpdate(this, Time.deltaTime);
            _statesDic[_currentState].CheckTransition(this, Time.deltaTime);
        }
        
        Debug.Log("isGrounded" + IsGrounded());

        // if (_anim)
        // {
        //     _anim.SetFloat("isWalking", rb.velocity.x);
        // }
        
        
        if (_currentLayerCollision != _layerCollision)
        {
            _currentLayerCollision = _layerCollision;
        }
        else
        {
            _currentLayerCollision = 0;
            _layerCollision = 0;
        }

    }

    public void TriggerAnimation(string animation)
    {
        _anim.SetTrigger(animation);
    }
    
    public void ToState(StateType newState)
    {
        if(newState == _currentState)
            return;
        
        if (_statesDic.ContainsKey(_currentState))
        {
            _statesDic[_currentState].OnExit(this);
        }
        
        _currentState = newState;

        if (_statesDic.ContainsKey(_currentState))
        {
            _statesDic[_currentState].OnEnter(this);
        }
    }

    private void Bind(FSMData fsmData)
    {
        foreach (FSMStateData stateData in fsmData.States)
        {
            State state = State.CreateState(stateData.StateType);
            if(state == null)
                continue;

            foreach (FSMTransitionData transitionData in stateData.Transition)
            {
                state.AddTransition(transitionData.TargetState, transitionData.Decision);
            }
            
            _statesDic.Add(stateData.StateType, state);
        }
    }
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        _layerCollision = col.collider.gameObject.layer;
    }
    
    public bool IsGrounded()
    {
        RaycastHit2D hit;
        float raycastDistance = 1.5f; // Adjust this distance based on your player's size
        hit = Physics2D.Raycast(enemy.position, Vector2.down, raycastDistance);
        if (hit.collider != null && hit.collider.gameObject.CompareTag("Ground"))
        {
            // Player is touching the ground
            return true;
        }
        return false;
    }
    
    
}