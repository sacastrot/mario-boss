using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    private float _posX;
    private float _posY;

    void Start()
    {
        _posX = PlayerPrefs.GetFloat("PositionX");
        _posY = PlayerPrefs.GetFloat("PositionY");
    }
    void Update()
    {
        Debug.Log("X " + _posX);
        Debug.Log("Y " + _posY);
    }
    public void LevelWithPlatforms()
    {
        SceneManager.LoadScene("PlatformLevelScene");
    }

    public void LevelWithEnemies()
    {
        SceneManager.LoadScene("EnemiesLevelScene");
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
