using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class FallOff : MonoBehaviour
{
    public int lifesValue = -1;
    public Transform spawnPoint;
    public Transform camera;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (ScoreManager.instance.totalLifes > 0)
            {
                ScoreManager.instance.ChangeLifesScore(lifesValue);
                ScoreManager.instance.ResetTotalScore();
                collision.gameObject.SetActive(false);
                camera.transform.position = spawnPoint.position;
                collision.gameObject.transform.position = spawnPoint.position;
                collision.gameObject.SetActive(true);
            }
            else
            {
                SceneManager.LoadScene("GameOver");
                ScoreManager.instance.ResetLifesScore();
                Destroy(collision.gameObject);
            }

        }
    }
}
