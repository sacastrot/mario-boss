using UnityEngine;

public class DeadState : State
{
    private float _deadDuration = 0;
    
    public DeadState() : base("Dead") { }

    public override StateType Type => StateType.Dead;
    
    protected override void OnEnterState(FiniteStateMachine fsm)
    {
        _deadDuration = fsm.Config.DeathDuration;
        fsm.TriggerAnimation("Death");
    }

    protected override void OnUpdateState(FiniteStateMachine fsm, float deltaTime)
    {
        if (_deadDuration >= 0)
        {
            _deadDuration -= deltaTime;
            if (_deadDuration <= 0)
            {
                fsm.gameObject.SetActive(false);
            }
        }
    }

    protected override void OnExitState(FiniteStateMachine fsm)
    {
    }
}