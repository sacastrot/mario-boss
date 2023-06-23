using UnityEngine;

[CreateAssetMenu(fileName = "OutOfFieldOfView Check", menuName = "FSM/Decisions/OutOfFieldOfView Check")]
public class OutOfFieldOfViewCheck: StateDecision
{
    public override bool Check(FiniteStateMachine fms)
    {
        Vector3 direction = (fms.target.position - fms.transform.position);
        float distance = direction.magnitude;
        if (distance > fms.Config.detectionRange*1.5)
        {
            return true;
        }
        return false;
    }
}