using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    public static ScoreManager instance;
    public TextMeshProUGUI text;
    private int coinScore;
    
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
            instance = this;
    }

    public void ChangeCoinScore(int coinValue)
    {
        coinScore += coinValue;
        text.text = coinScore.ToString();
    }

}
