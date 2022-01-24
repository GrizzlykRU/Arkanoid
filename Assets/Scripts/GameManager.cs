using System.Collections.Generic;
using System.Collections;
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

    private List<ObstacleCollision> _obstacles = new List<ObstacleCollision>();
    
    public static bool gamePaused = false;

    public static bool gameIsActive = false;

    public static bool gameOver = false;

    public static bool levelComplete = false;

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
                    _pauseMenuUI.SetActive(true);
                }
                else
                {
                    gamePaused = false;
                    _gameField.SetActive(true);
                    _pauseMenuUI.SetActive(false);
                }
            }

            if (Input.GetKeyDown(KeyCode.R) || obstacleCounter <= 0)
            {
                StartCoroutine(LevelComplete());
            }
        }
      
    }

    public IEnumerator LevelComplete()
    {
        //Time.timeScale = 0f;
        gameIsActive = false;
        levelComplete = true;
        yield return new WaitForSecondsRealtime(2.0f);
        _gameField.SetActive(false);
        if(levelNumber == _levels.Count-1)
        {
            _gameIsDoneUI.SetActive(true);
        }
        else
        {
            _completeLevelUI.SetActive(true);
        }
        yield break;
    }

    //public void GameOver() 
    //{
    //    //Time.timeScale = 0f;
    //    gameIsActive = false;
    //    _gameField.SetActive(false);
    //    _gameOverUI.SetActive(true);
    //}

    public IEnumerator GameOver()
    {
        gameIsActive = false;
        gameOver = true;
        yield return new WaitForSecondsRealtime(2.0f);   
        _gameField.SetActive(false);
        _gameOverUI.SetActive(true);
        yield break;
    }

    public void LoadLevel(int level)
    {
        if(_level != null)
        {
            levelComplete = false;
            gameOver = false;
            obstacleCounter = 0;
            _obstacles.Clear();
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
                    _obstacles.Add(child.gameObject.GetComponent<ObstacleCollision>());
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
