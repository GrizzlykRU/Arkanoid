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

    private GameObject _ball;

    public List<GameObject> _levels;

    public static bool gameIsActive = true;

    private int levelNumber = 0;

    private void Start()
    {
        _ball = GameObject.FindGameObjectWithTag("Ball");
        _obstacles = new List<GameObject>(GameObject.FindGameObjectsWithTag("Obstacle"));
        _ball.GetComponent<BallMovement>().obstacleCount = _obstacles.Count;
        _obstacles.Add(GameObject.FindGameObjectWithTag("Player"));
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
    }

    public void LevelComplete()
    {
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
        _gameOverlUI.SetActive(true);
    }

    public void LoadLevel(int level)
    {
        Destroy(_level);
        var obstaclesNum = 0;
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
                    obstaclesNum++;
                }
            }

        }
        _ball.GetComponent<BallMovement>().obstacleCount = obstaclesNum;
        _completeLevelUI.SetActive(false);
    }
    
}
