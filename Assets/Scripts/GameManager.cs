using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject completeLevelUI;

    public GameObject pauseMenuUI;

    //public List<GameObject> obstacles;

    //public GameObject ball;

    //public GameObject player;

    public static bool gameIsActive = true;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("PRESSED");
            if (gameIsActive)
            {
                gameIsActive = false;
                Time.timeScale = 0f;
                pauseMenuUI.SetActive(true);
            }
            else
            {
                gameIsActive = true;
                Time.timeScale = 1f;
                pauseMenuUI.SetActive(false);
            }
        }
    }

    public void LevelComplete()
    {
        completeLevelUI.SetActive(true);
    }

    public void GameOver() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
}
