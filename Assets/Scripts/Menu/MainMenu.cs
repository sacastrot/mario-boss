using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LevelWithPlatforms()
    {
        SceneManager.LoadScene("PlatformLevelScene");
    }

    public void LevelWithEnemies()
    {
        SceneManager.LoadScene("EnemiesLevelScene");
    }
}
