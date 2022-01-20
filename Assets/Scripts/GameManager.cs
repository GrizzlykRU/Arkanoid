using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject _completeLevelUI;

    public GameObject _pauseMenuUI;

    public GameObject _gameOverlUI;

    public GameObject _gameIsDoneUI;

    public GameObject _canvas;

    public GameObject _level;

    private List<GameObject> _obstacles;

    public static int obstacleCounter;

    private GameObject _ball;

    private GameObject _player;

    public List<GameObject> _levels;

    public GameObject _bonusBallPrefab;

    public GameObject _bonusPrefab;

    private GameObject _bonusBall;

    public static bool gameIsActive = true;

    private int levelNumber = 0;

    private void Start()
    {
        Time.timeScale = 1f;
        _ball = GameObject.FindGameObjectWithTag("Ball");
        _obstacles = new List<GameObject>(GameObject.FindGameObjectsWithTag("Obstacle"));
        obstacleCounter = _obstacles.Count;
        _player = GameObject.FindGameObjectWithTag("Player");
        _obstacles.Add(_player);
        _obstacles.AddRange(GameObject.FindGameObjectsWithTag("Border"));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("PRESSED");
            if (gameIsActive)
            {
                gameIsActive = false;
                Time.timeScale = 0f;
                _pauseMenuUI.SetActive(true);
            }
            else
            {
                gameIsActive = true;
                Time.timeScale = 1f;
                _pauseMenuUI.SetActive(false);
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            LevelComplete();
        }
    }

    private void FixedUpdate()
    {
        _ball.GetComponent<BallMovement>().GetCollision(_obstacles);
        if (_bonusBall != null)
        {
            _bonusBall.GetComponent<BallMovement>().GetCollision(_obstacles);
        }
        if (obstacleCounter == 0)
        {
            _ball.SetActive(false);
            LevelComplete();
        }
    }

    public void LevelComplete()
    {
        Time.timeScale = 0f;
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
        Time.timeScale = 0f;
        _gameOverlUI.SetActive(true);
    }

    public void LoadLevel(int level)
    {
        Destroy(_level);
        levelNumber = level;
        _level = Instantiate(_levels[level], _canvas.transform);
        _obstacles.Clear();
        foreach(Transform child in _level.transform)
        {
            if (child.tag == "Ball")
            {
                _ball = child.gameObject;
            }
            else
            {
                _obstacles.Add(child.gameObject);
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
        ballMovement._canvas = _canvas;
        ballMovement._gameManager = gameObject;
        Time.timeScale = 1.0f;
        _completeLevelUI.SetActive(false);
    }

    public void CreateBonus(Vector3 obstaclePosition)
    {
        int random = Random.Range(1, 10);
        Debug.Log(random);
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
        }
    }

}
