﻿public class WoundState : State {
    public override StateType Type { get; }
    public WoundState(string name) : base("Wound") { }

    protected override void OnEnterState(FiniteStateMachine fsm) {
        throw new System.NotImplementedException();
    }

    protected override void OnUpdateState(FiniteStateMachine fms, float deltaTime) {
        throw new System.NotImplementedException();
    }

    protected override void OnExitState(FiniteStateMachine fms) {
        throw new System.NotImplementedException();
    }

}