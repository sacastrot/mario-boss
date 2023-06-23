using UnityEngine;

[CreateAssetMenu(fileName = "AttackCollision Check", menuName = "FSM/Decisions/AttackCollision Check")]
public class AttackCollisionCheck: StateDecision {
    public override bool Check(FiniteStateMachine fsm) {
        return fsm.CollisionType != CollisionSide.None && fsm.CollisionType !=  CollisionSide.Top;
    }
}
