using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

    public int coinValue = 1;
    public int scoreValue = 1000;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            ScoreManager.instance.ChangeCoinScore(coinValue);
            ScoreManager.instance.ChangeTotalScore(scoreValue);
            Destroy(gameObject);
        }
    }
}
