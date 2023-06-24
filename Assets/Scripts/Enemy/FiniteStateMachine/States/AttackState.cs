using UnityEngine;

public class AttackState : State {
    public override StateType Type { get; }
    public AttackState() : base("Attack") { }

    public bool canAttack = true;

    protected override void OnEnterState(FiniteStateMachine fsm) {
        SetStateDuration(fsm.Config.attackDuration);
        canAttack = true;
    }

    protected override void OnUpdateState(FiniteStateMachine fms, float deltaTime) {
        if (fms.target.TryGetComponent(out IDamageable target)) {
            if (canAttack)
            {
                target.TakeHit(fms.Config.attackDamage);
                fms.rb.velocity.Set(0f, 0f);
                canAttack = false;
            }
            else
            {
                fms.ToState(StateType.Patrol);
            }
            
        }
  
        // if (fms.target.TryGetComponent(out IDamageable target)) {
        //     target.TakeHit(fms.Config.attackDamage);
        // }
    }

    protected override void OnExitState(FiniteStateMachine fms)
    {
        Debug.Log("Attack On exit");
    }
}