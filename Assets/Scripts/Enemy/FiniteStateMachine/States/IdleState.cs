using UnityEngine;

public class IdleState : State
{
    public IdleState() : base("Idle")
    {
    }

    public override StateType Type { get; }

    protected override void OnEnterState(FiniteStateMachine fsm)
    {
    }

    protected override void OnUpdateState(FiniteStateMachine fms, float deltaTime)
    {
    }

    protected override void OnExitState(FiniteStateMachine fms)
    {
    }
}