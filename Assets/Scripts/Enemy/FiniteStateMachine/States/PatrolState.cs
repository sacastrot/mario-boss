using System;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class PatrolState : State
{
    public override StateType Type { get; }
    public float currentSpeed;
    public int moveDirection = 1;


    public PatrolState() : base("Patrol")
    {
    }
    
    protected override void OnEnterState(FiniteStateMachine fsm)
    {
        SetMovementSpeed(fsm.Config.Speed);
    }

    private void SetMovementSpeed(float configSpeed)
    {
        currentSpeed = configSpeed;
    }

    protected override void OnUpdateState(FiniteStateMachine fsm, float deltaTime)
    {
        if (fsm.CurrentLayerCollision == 10) 
        {
            moveDirection *= -1;
        }
        if (moveDirection < 0)
        {
            fsm.enemy.localScale = new Vector2(-1, 1);
        }
        else if (moveDirection > 0)
        {
            fsm.enemy.localScale = new Vector2(1, 1);
        }
        fsm.rb.velocity = new Vector2(currentSpeed * moveDirection, fsm.rb.velocity.y);

        if (!fsm.IsGrounded())
        {
            fsm.TriggerAnimation("isFalling");
        }
        else
        {
            fsm.TriggerAnimation("isWalking");

        }
    }

    
    
    protected override void OnExitState(FiniteStateMachine fms)
    {
    }
    
    
}