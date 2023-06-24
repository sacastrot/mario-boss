using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveData : MonoBehaviour
{
    private Player _player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 8)
        {
            PlayerPrefs.SetFloat("PositionX", other.transform.position.x);
            PlayerPrefs.SetFloat("PositionY", other.transform.position.y);
            SceneManager.LoadScene("MainMenu");
            Debug.Log("Hiiiii");
        }
    }
}
