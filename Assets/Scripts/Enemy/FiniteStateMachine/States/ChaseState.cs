using Unity.VisualScripting;
using UnityEngine;

public class ChaseState : State {
    public override StateType Type { get; }
    public ChaseState() : base("Chase") { }
    private float _chaseSpeed;
    private float _moveDirection;
    private bool _turn;
    private float _turnTimer;
    
    protected override void OnEnterState(FiniteStateMachine fsm)
    {
        Vector3 position = (fsm.target.position - fsm.transform.position).normalized;
        _moveDirection = position.x;
        _turnTimer = fsm.Config.turnTimerChase;
        fsm.BoolAnimation("isTurningPP", false);
    }

    protected override void OnUpdateState(FiniteStateMachine fsm, float deltaTime) {
        //Chase
        Vector3 position = (fsm.target.position - fsm.transform.position).normalized;
        _chaseSpeed = IncreaseWithinBound(Mathf.Abs(fsm.rb.velocity.x), fsm.Config.chaseAcceleration,
            fsm.Config.maximumChaseSpeed);
        _turn = _moveDirection * position.x < 0;
        if (!_turn)
        { 
            
            if (fsm.hasRb) {
                fsm.enemy.localScale = new Vector2((int)(_moveDirection/Mathf.Abs(_moveDirection)), 1);
                fsm.rb.velocity = new Vector2(position.x * _chaseSpeed, fsm.rb.velocity.y);
            }
        
            // Animation of chaseState
            fsm.TriggerAnimation("isWalking");
        }
        else
        {
            fsm.rb.velocity = Vector2.zero;
            _turnTimer -= Time.deltaTime;
            if (_turnTimer <= 0)
            {
                _turn = false;
                _moveDirection = position.x;
                _turnTimer = fsm.Config.turnTimerChase;;
            }
            fsm.BoolAnimation("isTurningChase", _turn);
        }
    }

    protected override void OnExitState(FiniteStateMachine fms) { }

    private float DecreaseWithinBound(float actual, float delta, int target) {
        actual -= delta;
        if (actual < target) {
            actual = target;
        }

        return actual;
    }

    private float IncreaseWithinBound(float actual, float delta, float max) {
        actual += delta;
        if (actual > max) {
            actual = max;
        }

        return actual;
    }
}