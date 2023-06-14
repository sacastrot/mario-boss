﻿using System.Collections.Generic;
using UnityEngine;

public class FiniteStateMachine : MonoBehaviour {
    [Space(10)] [SerializeField] private Animator _anim;

    [Space(10)] [SerializeField] private Transform _target;

    [Space(10)] [SerializeField] public Rigidbody2D rb;

    [Space(10)] [SerializeField] public Transform enemy;

    private int _layerCollision;
    private int _currentLayerCollision;
    public Transform Target => _target;
    public EnemyConfig Config => _config;
    public int CurrentLayerCollision => _currentLayerCollision;

    public bool hasRb;
    private EnemyConfig _config;
    private Dictionary<StateType, State> _statesDic = new();
    private StateType _currentState;

    void Start() {
        _config = GetComponent<EnemyConfig>();

        hasRb = TryGetComponent<Rigidbody2D>(out rb);

        Bind(_config.fsmData);

        ToState(_config.initialState);
    }

    void Update() {
        if (_statesDic.ContainsKey(_currentState)) {
            _statesDic[_currentState].OnUpdate(this, Time.deltaTime);
            _statesDic[_currentState].CheckTransition(this, Time.deltaTime);
        }

        if (_currentLayerCollision != _layerCollision) {
            _currentLayerCollision = _layerCollision;
        } else {
            _currentLayerCollision = 0;
            _layerCollision = 0;
        }
    }

    public void TriggerAnimation(string animation) {
        _anim.SetTrigger(animation);
    }

    public void ToState(StateType newState) {
        if (newState == _currentState)
            return;

        if (_statesDic.ContainsKey(_currentState)) {
            _statesDic[_currentState].OnExit(this);
        }

        _currentState = newState;

        if (_statesDic.ContainsKey(_currentState)) {
            _statesDic[_currentState].OnEnter(this);
        }
    }

    private void Bind(FSMData fsmData) {
        foreach (FSMStateData stateData in fsmData.States) {
            State state = State.CreateState(stateData.StateType);
            if (state == null)
                continue;

            foreach (FSMTransitionData transitionData in stateData.Transition) {
                state.AddTransition(transitionData.TargetState, transitionData.Decision);
            }

            _statesDic.Add(stateData.StateType, state);
        }
    }

    private void OnCollisionEnter2D(Collision2D col) {
        _layerCollision = col.collider.gameObject.layer;
    }

    public bool IsGrounded() {
        float raycastDistance = 1f;
        RaycastHit2D hit = Physics2D.Raycast(enemy.position, Vector2.down, raycastDistance);
        return hit.collider != null && hit.collider.gameObject.CompareTag("Ground");
    }
}