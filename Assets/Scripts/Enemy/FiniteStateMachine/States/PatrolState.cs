using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class PatrolState : State
{
    public override StateType Type { get; }

    public PatrolState() : base("Patrol")
    {
    }

    private float _moveDirection;

    protected override void OnEnterState(FiniteStateMachine fsm)
    {
        fsm.SetMovementSpeed(fsm.Config.Speed);
    }

    protected override void OnUpdateState(FiniteStateMachine fsm, float deltaTime)
    {
        fsm.rb.velocity = new Vector2(fsm.currentSpeed * fsm.moveDirection, fsm.rb.velocity.y);

        if (fsm.moveDirection < 0)
        {
            fsm.enemy.localScale = new Vector2(-1, 1);
        }
        else if (fsm.moveDirection > 0)
        {
            fsm.enemy.localScale = new Vector2(1, 1);
        }
    }

    protected override void OnExitState(FiniteStateMachine fms)
    {
    }
}