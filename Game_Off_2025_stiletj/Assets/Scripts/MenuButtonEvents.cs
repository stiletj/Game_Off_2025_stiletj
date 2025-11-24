using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonEvents : MonoBehaviour
{
    private GameObject gameManager;
    private bool inGame = false;

    public void Start()
    {
        gameManager = GameObject.Find("GameManager");

        if (gameManager != null)
        {
            inGame = true;
        }
    }

    public void Update()
    {
        
    }

    public void Resume()
    {
        if (inGame)
        {
            gameManager.GetComponent<GameManager>().ResumeGame();
        }
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void EndGame()
    {
        PlayerPrefs.SetInt("LastScore", Mathf.RoundToInt(gameManager.GetComponent<GameManager>().gameScore));
        PlayerPrefs.SetInt("LastMin", gameManager.GetComponent<GameManager>().FinalMin());
        PlayerPrefs.SetInt("LastSec", gameManager.GetComponent<GameManager>().FinalSec());
        PlayerPrefs.SetFloat("LastMs", gameManager.GetComponent<GameManager>().FinalMs());

        SceneManager.LoadScene(2);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
