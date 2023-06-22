using UnityEngine;

public class EnemyConfig: MonoBehaviour
{
    public int Health = 2;
    
    [Header("Movement")] 
    public float speed = 5.0f;
    public float maximumChaseSpeed = 6f;
    public float chaseAcceleration = 2f;
    public float jumpVelocity = 15f;
    public float pingPongDistance = 5.0f;
    public float pingPongTime = 2.45f;
    public float turnTimerChase = 0.8f;
    public int moveDirection = 1;

    [Header("Detection Range")]
    public float detectionRange = 5.0f;
    public float tauntDuration = 2.0f;
    
    [Header("Attack")]
    public int attackDamage = 1;
    // Temp
    public float attackDuration = 2f;
    
    [Header("Dead")]
    public float DeathDuration = 0f;

    [Header("Finite-State Machine")]
    public StateType initialState;
    public FSMData fsmData;
    
    
}