using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public GameManager _gameManager;

    public GameObject _mainMenuUI;

    public GameObject _gameField;

    public void Retry()
    {
        _gameManager.LoadLevel(0);
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    public void Quit()
    {
        _mainMenuUI.SetActive(true);
        _gameField.SetActive(false);
        gameObject.SetActive(false);
        GameManager.gameIsActive = false;
    }
}
