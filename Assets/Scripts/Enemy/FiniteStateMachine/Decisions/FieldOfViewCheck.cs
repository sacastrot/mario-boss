using UnityEngine;

[CreateAssetMenu(fileName = "FieldOfView Check", menuName = "FSM/Decisions/FieldOfView Check")]
public class FieldOfViewCheck: StateDecision
{
    public override bool Check(FiniteStateMachine fms)
    {
        Vector3 direction = (fms.Target.position - fms.transform.position);
        float distance = direction.magnitude;
        if (distance <= fms.Config.detectionRange)
        {
            return true;
        }
        return false;
    }
}
