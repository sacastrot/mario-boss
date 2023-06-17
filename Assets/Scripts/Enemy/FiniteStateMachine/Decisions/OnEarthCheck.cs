using UnityEngine;

[CreateAssetMenu(fileName = "OnEarth Check", menuName = "FSM/Decisions/OnEarth Check")]
public class OnEarthCheck:StateDecision
{
    public override bool Check(FiniteStateMachine fsm)
    {
        return fsm.IsGrounded();
    }
}
