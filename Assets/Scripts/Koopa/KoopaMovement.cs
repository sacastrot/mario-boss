using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KoopaMovement : MonoBehaviour
{
    
    private float speed= 0.7f;
    private bool direccion;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(direccion)
        {
            transform.Translate(2*Time.deltaTime*speed,0,0);
        }
        else
        {
            transform.Translate(-2*Time.deltaTime*speed,0,0);
        }
    }


    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.gameObject.layer == 8) // 8 is the layer of pipes
        {
            direccion = !direccion;
            if (direccion)
            {
                transform.localScale = new Vector2(-1, 1);
            }
            else
            {
                transform.localScale = new Vector2(1, 1);
            }
            
        }
    }
}
