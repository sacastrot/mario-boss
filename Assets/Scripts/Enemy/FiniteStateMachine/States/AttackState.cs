using UnityEngine;

public class AttackState : State {
    public override StateType Type { get; }
    public AttackState() : base("Attack") { }

    protected override void OnEnterState(FiniteStateMachine fsm) {
        
        Debug.Log("Enter attack state");
        SetStateDuration(fsm.Config.attackDuration);
    }

    protected override void OnUpdateState(FiniteStateMachine fms, float deltaTime) {
        if (fms.Target.TryGetComponent(out IDamageable target)) {
            target.TakeHit(fms.Config.attackDamage);
        }
    }

    protected override void OnExitState(FiniteStateMachine fms) { }
}