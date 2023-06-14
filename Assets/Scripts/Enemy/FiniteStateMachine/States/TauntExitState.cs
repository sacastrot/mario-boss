using UnityEngine;

public class TauntExitState : State {
    public TauntExitState() : base("TauntExit") { }
    public override StateType Type { get; }

    protected override void OnEnterState(FiniteStateMachine fsm) {
        // Trigger animation of jump, with this animation exit of taunt state
        fsm.TriggerAnimation("isJump");

        if (fsm.hasRb) {
            fsm.rb.bodyType = RigidbodyType2D.Dynamic;
            fsm.rb.AddForce(Vector2.up * fsm.Config.jumpVelocity, ForceMode2D.Impulse);
            fsm.rb.gravityScale = 3f;
            fsm.rb.freezeRotation = true;
        }
    }

    protected override void OnUpdateState(FiniteStateMachine fsm, float deltaTime) { }

    protected override void OnExitState(FiniteStateMachine fms) { }
}