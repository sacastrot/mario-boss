using UnityEngine;

public class TauntState : State {
    public override StateType Type { get; }
    public TauntState() : base("Taunt") { }

    protected override void OnEnterState(FiniteStateMachine fsm) {
        SetStateDuration(fsm.Config.tauntDuration);
        fsm.TriggerAnimation("isTaunt");
    }

    protected override void OnUpdateState(FiniteStateMachine fms, float deltaTime) { }

    protected override void OnExitState(FiniteStateMachine fms) { }
}