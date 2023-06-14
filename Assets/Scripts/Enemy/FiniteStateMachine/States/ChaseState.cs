using Unity.VisualScripting;
using UnityEngine;

public class ChaseState : State {
    public override StateType Type { get; }
    public ChaseState() : base("Chase") { }
    private float _chaseSpeed;
    protected override void OnEnterState(FiniteStateMachine fsm) { }

    protected override void OnUpdateState(FiniteStateMachine fsm, float deltaTime) {
        // Animation of chaseState
        fsm.TriggerAnimation("isWalking");

        //Chase
        Vector3 position = (fsm.Target.position - fsm.transform.position).normalized;
        _chaseSpeed = IncreaseWithinBound(Mathf.Abs(fsm.rb.velocity.x), fsm.Config.chaseAcceleration,
            fsm.Config.maximumChaseSpeed);


        if (fsm.hasRb) {
            fsm.rb.velocity =
                new Vector2(position.x * _chaseSpeed, fsm.rb.velocity.y);
        }

        if (position.x < 0) {
            fsm.enemy.localScale = new Vector2(-1, 1);
        } else if (position.x > 0) {
            fsm.enemy.localScale = new Vector2(1, 1);
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