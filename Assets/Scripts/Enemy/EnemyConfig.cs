using UnityEngine;

public class EnemyConfig: MonoBehaviour
{
    public int Health = 2;
    
    [Header("Movement")] 
    public float speed = 5.0f;
    public float maximumChaseSpeed = 6f;
    public float chaseAcceleration = 2f;
    public float jumpVelocity = 15f;
    public float PingPongDistance = 5.0f;
    public float PingPongTime = 4.5f;

    [Header("Detection Range")]
    public float detectionRange = 5.0f;
    public float tauntDuration = 2.0f;
    
    [Header("Attack")]
    public int attackDamage = 1;
    // Temp
    public float attackDuration = 2f;

    [Header("Finite-State Machine")]
    public StateType initialState;
    public FSMData fsmData;
}