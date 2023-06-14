using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : LivingEntity
{
    void Start()
    {
        InitHealth();
    }
    void Update()
    {
        
    }
    protected override void OnDeath() {
        base.OnDeath();
        gameObject.SetActive(false);
    }
}
