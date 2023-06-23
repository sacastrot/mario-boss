using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class makeDamage : MonoBehaviour
{
    public int amount = 1;
    
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.gameObject.layer == 30)
        {
            if(transform.position.y <= col.transform.position.y)  //le coloque el igual para hacerlo mas dificil xdxd
            {
                Destroy(gameObject);
            }
            else
            {
                col.collider.gameObject.GetComponent<health_and_damage>().TakeDamage(amount);
            }
            
        }
    }
}
