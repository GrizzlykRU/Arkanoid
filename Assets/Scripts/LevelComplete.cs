using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{
    public GameManager _gameManager;

    private int level = 0;

    public void NextLevel()
    {
        _gameManager.LoadLevel(++level);
    }

    // Start is called before the first frame update
    public void Retry()
    {
        _gameManager.LoadLevel(level);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
