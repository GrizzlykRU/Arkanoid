using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;


    // Start is called before the first frame update
    public void Resume()
    {
        GameManager.gameIsActive = true;
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
        GameManager.gameIsActive = true;
    }
}