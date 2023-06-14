using UnityEngine;

public class ChaseState:State
{
    public override StateType Type { get; }
    public ChaseState() : base("Chase") { }

    private float _chaseSpeed;
    private Rigidbody2D _rb;
    private Transform _target;
    protected override void OnEnterState(FiniteStateMachine fsm)
    {
        _chaseSpeed = fsm.Config.ChaseSpeed;
        if (fsm.hasRb)
        {
            _rb = fsm.rb;
        }

        _target = fsm.Target;
    }

    protected override void OnUpdateState(FiniteStateMachine fms, float deltaTime)
    {
        fms.TriggerAnimation("isWalking");
        Vector3 position = (_target.position - fms.transform.position).normalized;
        if (position.x < 0)
        {
            fms.enemy.localScale = new Vector2(-1, 1);
        } else if (position.x > 0)
        {
            fms.enemy.localScale = new Vector2(1, 1);
        }

        _rb.velocity = new Vector2(position.x * _chaseSpeed, _rb.velocity.y);
    }

    protected override void OnExitState(FiniteStateMachine fms)
    {
    }
}