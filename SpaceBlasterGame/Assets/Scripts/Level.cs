using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] float DelayInSeconds = 2;
    public void LoadStartMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("GameScene");
        FindObjectOfType<GameSession>().ResetGameSession();
    }

    public void LoadGameOver()
    {
        StartCoroutine(GameOverDelay());
    }
    IEnumerator GameOverDelay()
    {
        
        yield return new WaitForSeconds(DelayInSeconds);
        SceneManager.LoadScene("GameOver");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
