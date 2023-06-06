﻿using System.Collections.Generic;
using UnityEngine;

public class FiniteStateMachine : MonoBehaviour
{
    [Space(10)]
    [SerializeField] private Animator _anim;
    
    [Space(10)]
    [SerializeField] private Transform _target;
    
    [Space(10)]
    [SerializeField] public Rigidbody2D rb;

    [Space(10)]
    [SerializeField] public Transform enemy;
    

    public Transform Target => _target;
    public EnemyConfig Config => _config;

    private bool _hasRb;

    
    private EnemyConfig _config;
    private Dictionary<StateType, State> _statesDic = new();
    private StateType _currentState;
    public float currentSpeed = 1;
    public float moveDirection = 1;

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
        
        if (_anim)
        {
            //TODO manage animator 
        }
    }

    public void TriggerAnimation(string animation)
    {
        _anim.SetTrigger(animation);
    }

    public void SetMovementSpeed(float speed)
    {
        currentSpeed = speed;
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
        if (col.collider.gameObject.layer == 8) // 8 is the layer of pipes
        {
            moveDirection *= -1;
        }
    }
}