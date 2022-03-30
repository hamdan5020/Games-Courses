using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class GameSession : MonoBehaviour
{

    int score = 0;
    void Awake()
    {
        SetUpSingleton();
    }

    private void SetUpSingleton()
    {

        int numOfGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numOfGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

  
    
    public int GetScore()
    {
        return score;
    }

    public void AddToScore(int score)
    {
        this.score += score;
    }

    public void ResetGameSession()
    {
        Destroy(gameObject);
    }
}
