using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NumberWizzard : MonoBehaviour
{

    [SerializeField] int max;
    [SerializeField] int min;
    [SerializeField] TextMeshProUGUI textGuess;
    int guess;

    // Start is called before the first frame update
    void Start()
    {
        NextGuess();
    }
    public void OnPressHigher()
    {
        min = guess + 1;
        NextGuess();
    }
    public void OnPressLower()
    {
        max = guess - 1;
        NextGuess();
    }

    public void NextGuess()
    {
        guess = Random.Range(min, max + 1);
        textGuess.text = guess.ToString();
    }
}
