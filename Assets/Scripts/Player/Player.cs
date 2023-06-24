using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
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

    protected override void OnTakeDamage()
    {
        base.OnTakeDamage();
        Debug.Log("Take damage");
        gameObject.TryGetComponent(out PlayerController controller);
        controller.grownUp = false;
    }

    protected override void OnDeath() {
        base.OnDeath();
        // gameObject.SetActive(false);
        Debug.Log("isDeath");
    }
}
