using UnityEngine;

[CreateAssetMenu(fileName = "AttackCollision Check", menuName = "FSM/Decisions/AttackCollision Check")]
public class AttackCollisionCheck: StateDecision {
    public override bool Check(FiniteStateMachine fsm) {
        bool decision = fsm.CollisionType != CollisionSide.None && fsm.CollisionType != CollisionSide.Top;
        fsm._collisionType = CollisionSide.None;
        return decision;
    }
}
