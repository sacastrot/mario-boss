using UnityEngine;

public class AttackState: State
{
    public override StateType Type { get; }
    
    public AttackState() : base("Attack") { }

    protected override void OnEnterState(FiniteStateMachine fsm)
    {
    }

    protected override void OnUpdateState(FiniteStateMachine fsm, float deltaTime)
    {
        //Apply Damage
        if (fsm.Target.TryGetComponent(out IDamageable target))
        {
            target.TakeHit(fsm.Config.AttackDamage);
        }
        //These lines must be deleted
        fsm.TriggerAnimation("isFalling"); 
        fsm.rb.velocity = new Vector2(0, fsm.rb.velocity.y);
    }

    protected override void OnExitState(FiniteStateMachine fsm)
    {
    }
}