using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateType {None, Patrol, Chase, Attack, Taunt, TauntExit, Idle, Dead, Wound, PatrolPingPong}

public abstract class State
{
    [HideInInspector] public string name = "None";
    private List<StateTransition> _transitions = new List<StateTransition>();

    private float _stateDuration;
    private float _stateTimer;

    public State(string name) => this.name = name;
    
    public abstract StateType Type { get; }
    protected abstract void OnEnterState(FiniteStateMachine fsm);
    protected abstract void OnUpdateState(FiniteStateMachine fsm, float deltaTime);
    protected abstract void OnExitState(FiniteStateMachine fsm);
    
    public void OnEnter(FiniteStateMachine fsm)
    {
        OnEnterState(fsm);
        _stateTimer = _stateDuration;
    }
    
    public void OnUpdate(FiniteStateMachine fsm, float deltaTime)
    {
        OnUpdateState(fsm, deltaTime);
    }
    
    public void OnExit(FiniteStateMachine fsm)
    {
        OnExitState(fsm);
    }

    public void CheckTransition(FiniteStateMachine fsm, float deltaTime)
    {
        _stateTimer -= deltaTime;
        
        if(_stateTimer > 0)
            return;

        _stateTimer = 0;
        
        for (int i = 0; i < _transitions.Count; i++)
        {
            if (_transitions[i].Check(fsm))
            {
                fsm.ToState(_transitions[i].TargetState);
                break;
            }
        }
    }

    protected void SetStateDuration(float time)
    {
        _stateDuration = time;
    }
    
    public void AddTransition(StateType targetState, StateDecision decision)
    {
        _transitions.Add(new StateTransition
        {
            TargetState = targetState,
            Decision = decision
        });
    }
    
    public static State CreateState(StateType stateType)
    {
        switch (stateType)
        {
            case StateType.Patrol:
                return new PatrolState();
            case StateType.Idle:
                return new IdleState();
            case StateType.Chase:
                return new ChaseState();
            case StateType.Attack:
                return new AttackState();
            case StateType.Taunt:
                return new TauntState();
            case StateType.TauntExit:
                return new TauntExitState();
            case StateType.Dead:
                return new DeadState();
            // case StateType.Wound:
            //     return new WoundState();
            case StateType.PatrolPingPong:
                return new PatrolPingPongState();
        }

        return null;
    }
}
