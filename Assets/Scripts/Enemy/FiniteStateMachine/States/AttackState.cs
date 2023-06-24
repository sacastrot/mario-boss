using UnityEngine;

public class AttackState : State {
    public override StateType Type { get; }
    public AttackState() : base("Attack") { }

    private float _timeToPatrol = 2;
    private float _timer;

    protected override void OnEnterState(FiniteStateMachine fsm) {
        SetStateDuration(fsm.Config.attackDuration);
        _timer = _timeToPatrol;
    }

    protected override void OnUpdateState(FiniteStateMachine fms, float deltaTime) {
        if (fms.target.TryGetComponent(out IDamageable target) && _timer == _timeToPatrol) {
            target.TakeHit(fms.Config.attackDamage);
            fms.rb.velocity = Vector2.zero;
        }
        else if (_timer <= 0){
            fms.ToState(StateType.Patrol);
        }
        _timer -= Time.deltaTime;
    }

    protected override void OnExitState(FiniteStateMachine fms) { }
}