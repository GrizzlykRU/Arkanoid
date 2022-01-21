using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject _settingsUI;

    public GameObject _menuUI;

    public GameManager _gameManager;

    public void StartGame()
    {
        _gameManager.LoadLevel(0);
        _menuUI.SetActive(false);
    }
    public void Settings()
    {
        _settingsUI.SetActive(true);
        gameObject.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
