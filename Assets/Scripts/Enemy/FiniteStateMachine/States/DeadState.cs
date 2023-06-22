using UnityEngine;

public class DeadState : State
{
    private float _deadDuration = 0;
    
    public DeadState() : base("Dead") { }

    public override StateType Type => StateType.Dead;
    
    protected override void OnEnterState(FiniteStateMachine fms)
    {
        _deadDuration = fms.Config.DeathDuration;
        fms.TriggerAnimation("Death");
    }

    protected override void OnUpdateState(FiniteStateMachine fms, float deltaTime)
    {
        if (_deadDuration >= 0)
        {
            _deadDuration -= deltaTime;
            if (_deadDuration <= 0 && fms.enemy.TryGetComponent(out IDamageable enemy))
            {
                enemy.TakeHit(enemy.TotalHealthPoints);
            }
        }
    }

    protected override void OnExitState(FiniteStateMachine fms)
    {
    }
}