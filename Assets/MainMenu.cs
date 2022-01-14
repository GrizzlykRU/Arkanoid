using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuUI;

    public GameObject settingsUI;

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Settings()
    {
        settingsUI.SetActive(true);
        mainMenuUI.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
