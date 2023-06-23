using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class health_and_damage : MonoBehaviour
{
    public int health = 1;
    
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
    
}
