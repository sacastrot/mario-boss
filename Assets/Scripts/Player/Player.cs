using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : LivingEntity
{
    public int lifesValue = -1;
    public Transform spawnPoint;
    
    void Start()
    {
        InitHealth();
    }
    void Update()
    {
        
    }

    protected override void OnTakeDamage()
    {
        base.OnTakeDamage();
        Debug.Log("Take damage");
        gameObject.TryGetComponent(out PlayerController controller);
        controller.grownUp = false;
    }

    protected override void OnDeath() {
        base.OnDeath();
        if (ScoreManager.instance.totalLifes > 0)
        {
            ScoreManager.instance.ChangeLifesScore(lifesValue);
            ScoreManager.instance.ResetTotalScore();
            // DontDestroyOnLoad(ScoreManager);
            SceneManager.LoadScene("EnemiesLevelScene");
            // camera.transform.position = spawnPoint.position;
            // this.gameObject.transform.position = spawnPoint.position;
            // this.gameObject.SetActive(true);
        }
        else
        {   
            ScoreManager.instance.ChangeLifesScore(lifesValue);
            ScoreManager.instance.ResetLifesScore();
            SceneManager.LoadScene("GameOver");
            // gameObject.SetActive(false);
        }
        
    }
}
