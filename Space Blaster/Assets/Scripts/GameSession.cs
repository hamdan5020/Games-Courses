using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameSession : MonoBehaviour
{   
    [Range(0.1f, 10f)] [SerializeField] float GameSpeed = 1f;
    [SerializeField] int pointsPerBlock  = 30;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] int crntScore = 0;
    [SerializeField] bool autoPlay;

    private void Awake()
    {
        int gameStatusCount = FindObjectsOfType<GameSession>().Length;

        if (gameStatusCount > 1) 
        {
            gameObject.SetActive(false);
            Destroy(gameObject);


        }
        else 
        {
            DontDestroyOnLoad(gameObject);

        }
    }
    private void Start()
    {
        scoreText.text = crntScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = GameSpeed;

    }

    public void AddToScore()
    {
        crntScore += pointsPerBlock;
        scoreText.text = crntScore.ToString();

    }

    public void ResetGame()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    public bool IsAutoPlayEnabled()
    {
        return autoPlay;
    }
}
