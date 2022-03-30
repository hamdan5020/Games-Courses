using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Security.Cryptography;

public class BaseHealthDisplay : MonoBehaviour
{
    [SerializeField] float baseLives = 3;   
    [SerializeField] int damagePerAttacker = 100;
    [SerializeField] float baseDmgRedTime = 0.1f;

    Text baseHealthText;
    float baseHealth;
    // Start is called before the first frame update
    void Start()
    {
        baseHealth = baseLives - PlayerPrefsController.GetDifficulty();
        baseHealthText = GetComponent<Text>();
        UpdateDisplay();
    }


    private void UpdateDisplay()
    {
        baseHealthText.text = baseHealth.ToString();
    }

    private void DamageBase(int damageAmount)
    {
        baseHealth -= damageAmount;
        GetComponent<Text>().color = Color.red;
        StartCoroutine(WaitForTime());
        UpdateDisplay();

        if (baseHealth <= 0)
        {
            FindObjectOfType<LevelController>().HandleLoseCondition();
        }
    }

    IEnumerator WaitForTime()
    {
        yield return new WaitForSeconds(baseDmgRedTime);
        GetComponent<Text>().color = Color.green;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageBase(damagePerAttacker);
        Destroy(collision.gameObject);
    }
}
