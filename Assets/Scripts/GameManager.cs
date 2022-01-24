using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject _completeLevelUI;

    public GameObject _pauseMenuUI;

    public GameObject _gameOverUI;

    public GameObject _gameIsDoneUI;

    public GameObject _gameField;

    public GameObject _level;

    public static int obstacleCounter = 0;

    private GameObject _ball;

    private GameObject _player;

    public List<GameObject> _levels;

    public GameObject _bonusBallPrefab;

    public GameObject _bonusPrefab;

    private GameObject _bonusBall;

    public static bool gamePaused = false;

    public static bool gameIsActive = false;

    public static int levelNumber = 0;

    private void FixedUpdate()
    {
        if (gameIsActive)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Debug.Log("PRESSED");
                if (!gamePaused)
                {
                    gamePaused = true;
                    _gameField.SetActive(false);
                    //Time.timeScale = 0f;
                    _pauseMenuUI.SetActive(true);
                }
                else
                {
                    gamePaused = false;
                    _gameField.SetActive(true);
                    //Time.timeScale = 1f;
                    _pauseMenuUI.SetActive(false);
                }
            }

            if (Input.GetKeyDown(KeyCode.R) || obstacleCounter <= 0)
            {
                LevelComplete();
            }
        }
      
    }

    public void LevelComplete()
    {
        //Time.timeScale = 0f;
        gameIsActive = false;
        _gameField.SetActive(false);
        if(levelNumber == _levels.Count-1)
        {
            _gameIsDoneUI.SetActive(true);
        }
        else
        {
            _completeLevelUI.SetActive(true);
        }
    }

    public void GameOver() 
    {
        //Time.timeScale = 0f;
        gameIsActive = false;
        _gameField.SetActive(false);
        _gameOverUI.SetActive(true);
    }

    public void LoadLevel(int level)
    {
        if(_level != null)
        {
            obstacleCounter = 0;
            Destroy(_level);
        }

        levelNumber = level;
        _level = Instantiate(_levels[level], _gameField.transform);

        foreach (Transform child in _level.transform)
        {
            if (child.tag == "Ball")
            {
                _ball = child.gameObject;
            }
            else
            {
                if(child.tag == "Obstacle")
                {
                    obstacleCounter++;
                }
                else if (child.tag == "Player")
                {
                    _player = child.gameObject;
                }
            }
        }
        var ballMovement = _ball.GetComponent<BallMovement>();
        ballMovement._player = _player;
        ballMovement._gameField = _gameField;
        ballMovement._gameManager = this;
        _gameField.SetActive(true);
        gameIsActive = true;
        Debug.Log("Start game : " + obstacleCounter);
        //Time.timeScale = 1.0f;
        //_completeLevelUI.SetActive(false);
    }

    public void CreateBonus(Vector3 obstaclePosition)
    {
        int random = Random.Range(1, 10);
        if(random >= 5)
        {
            var bonus = Instantiate(_bonusPrefab, obstaclePosition, new Quaternion(), _level.transform);
        }
    }

    public void CreateBonusBall()
    {
        if (_bonusBall != null)
        {
            _bonusBall.GetComponent<BonusBallMovement>().startTime = System.DateTime.Now;
        }
        else
        {
            _bonusBall = Instantiate(_bonusBallPrefab, _level.transform);
            _bonusBall.GetComponent<BonusBallMovement>()._gameManager = this;
        }
    }

}
