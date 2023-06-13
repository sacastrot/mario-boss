using UnityEngine;

[CreateAssetMenu(fileName = "InRangeAttack Check", menuName = "FSM/Decisions/InRangeAttack Check")]
public class InAttackRangeCheck : StateDecision
{
    public override bool Check(FiniteStateMachine fsm)
    {
        float distance = (fsm.Target.position - fsm.enemy.position).magnitude;
        // Debug.Log("Decision: magnitude "  + distance);
        return distance <= fsm.Config.AttackRange;
    }
}