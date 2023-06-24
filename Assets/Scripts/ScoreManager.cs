using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    public static ScoreManager instance;
    public TextMeshProUGUI textCoin;
    public int coinScore;
    public TextMeshProUGUI textTotalScore;
    public int totalScore;
    public TextMeshProUGUI textMarioLifes;
    public int totalLifes;
    
    // Start is called before the first frame update
    // private void Awake()
    // {
    //     instance = this;
    //     DontDestroyOnLoad(gameObject);
    // }    
    void Start()
    {
        if (instance == null)
            instance = this;

        textMarioLifes.text = totalLifes.ToString();
        if (PlayerPrefs.GetInt("totalLifes") >= 0)
            totalLifes = PlayerPrefs.GetInt("totalLifes");
        else
            totalLifes = 6;
    }

    // private void Update()
    // {
    //     
    // }

    public void ChangeCoinScore(int coinValue)
    {
        coinScore += coinValue;
        textCoin.text = coinScore.ToString();
    }

    public void ChangeTotalScore(int valueScore)
    {
        totalScore += valueScore;
        textTotalScore.text = totalScore.ToString();
    }

    public void ChangeLifesScore(int lifesValue)
    {
        totalLifes += lifesValue;
        textMarioLifes.text = totalLifes.ToString();
    }

    public void ResetLifesScore()
    {
        totalLifes = 6;
        PlayerPrefs.SetInt("totalLifes", -1);
        textMarioLifes.text = totalLifes.ToString();
    }
    
    public void ResetTotalScore()
    {
        totalScore = 0;
        PlayerPrefs.SetInt("totalLifes", totalLifes);
        textTotalScore.text = totalScore.ToString();
    }

}
