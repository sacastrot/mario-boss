using UnityEngine;

[CreateAssetMenu(fileName = "OutOfFieldOfView Check", menuName = "FSM/Decisions/OutOfFieldOfView Check")]
public class OutOfFieldOfViewCheck: StateDecision
{
    public override bool Check(FiniteStateMachine fms)
    {
        Vector3 direction = (fms.Target.position - fms.transform.position);
        float distance = direction.magnitude;
        if (distance > fms.Config.detectionRange*2)
        {
            return true;
        }
        return false;
    }
}