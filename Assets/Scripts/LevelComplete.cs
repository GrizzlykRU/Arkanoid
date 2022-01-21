using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{
    public GameManager _gameManager;

    public void NextLevel()
    {
        _gameManager.LoadLevel(GameManager.levelNumber+1);
        gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    public void Retry()
    {
        _gameManager.LoadLevel(GameManager.levelNumber);
        gameObject.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
