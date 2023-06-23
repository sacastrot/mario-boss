using UnityEngine;

[CreateAssetMenu(fileName = "TopCollision Check", menuName = "FSM/Decisions/TopCollision Check")]
public class TopCollisionCheck: StateDecision {
    public override bool Check(FiniteStateMachine fsm)
    {
        if (fsm.CollisionType == CollisionSide.Top)
        {
            return true;
        }

        return false;
    }
}