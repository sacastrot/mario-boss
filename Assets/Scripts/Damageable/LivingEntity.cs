﻿using System;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamageable
{
    [field: SerializeField] public int TotalHealthPoints { get; protected set; } = 1;
    public int HealthPoints { get; private set; }
    
    public void TakeHit(int damage)
    {
        if(HealthPoints <= 0)
            return;
    
        HealthPoints -= damage;
        OnTakeDamage();
        
        if (HealthPoints <= 0)
        {
            OnDeath();
        }
    }

    protected void InitHealth()
    {
        HealthPoints = TotalHealthPoints;
    }
    
    protected virtual void OnTakeDamage(){ }
    protected virtual void OnDeath(){ }
}