using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionBlock : MonoBehaviour
{
    public GameObject block;
    public Transform objectSpawn;
    public GameObject powerUp;
    [Header("Player")] [SerializeField]
    public Transform player;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == 8 && col.contacts[0].normal.y > 0.5f)
        {
            Instantiate(block, objectSpawn.position, objectSpawn.rotation);
            GameObject star = Instantiate(powerUp, objectSpawn.position, objectSpawn.rotation);
            StarMovement starMovement = star.GetComponent<StarMovement>();
            starMovement.playerController = player.GetComponent<PlayerController>();
            Destroy(gameObject);
        }
    }
}
