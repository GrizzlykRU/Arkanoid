using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject _mainMenuUI;

    public GameObject _gameField;

    // Start is called before the first frame update
    public void Resume()
    {
        GameManager.gamePaused = false;
        _gameField.SetActive(true);
        //Time.timeScale = 1f;
        gameObject.SetActive(false);
    }

    public void ReturnToMainMenu()
    {
        _mainMenuUI.SetActive(true);
        _gameField.SetActive(false);
        gameObject.SetActive(false);
        //Time.timeScale = 1f;
        GameManager.gameIsActive = false;
    }
}
