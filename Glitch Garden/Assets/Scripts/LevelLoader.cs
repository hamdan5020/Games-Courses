using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] int timeToWait = 4;
    int crntSceneIndex;
    
    // Start is called before the first frame update
    void Start()
    {
        crntSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (crntSceneIndex == 0)
        {
            StartCoroutine(WaitForTime());
        }
        
    }

        IEnumerator WaitForTime()
    {
        yield return new WaitForSeconds(timeToWait);
        LoadNextScene();
    }

    public void RestartScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(crntSceneIndex);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Start Screen");
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(crntSceneIndex + 1);
    }

    public void LoadOptionsScene()
    {
        SceneManager.LoadScene("Options Screen");
    }

   public void QuitGame()
    {
        Application.Quit();
    }

}
