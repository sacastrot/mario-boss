﻿using UnityEngine;

public class EnemyConfig: MonoBehaviour
{
    public int Health = 2;
    
    [Header("Movement")] 
    public float Speed = 5.0f;
    public float ChaseSpeed = 2.7f;
    public float PingPongDistance = 5.0f;
    public float PingPongTime = 4.5f;

    [Header("Detection Range")]
    public float DetectionRange = 5.0f;
    public float TauntDuration = 2.0f;
    
    [Header("Attack")]
    public int AttackDamage = 1;
    public float AttackRange = 2.2f;
    public float AttackDuration = 0;

    [Header("Finite-State Machine")]
    public StateType InitialState;
    public FSMData FSMData;

}