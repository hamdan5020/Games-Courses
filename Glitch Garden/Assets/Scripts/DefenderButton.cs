using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Sprites;
using UnityEngine.UI;

public class DefenderButton : MonoBehaviour
{
    [SerializeField] Defender defenderPrerab;

    private void Start()
    {
        LabelButtonsWithCost();
    }

    private void LabelButtonsWithCost()
    {
        Text costText = GetComponentInChildren<Text>();
        if (!costText)
        {
            Debug.LogError(name + " has no cost text");
        }else
        {
            costText.text = defenderPrerab.GetStarCost().ToString();
        }
    }

    private void OnMouseDown()
    {
        var buttons = FindObjectsOfType<DefenderButton>();
        foreach(DefenderButton button in buttons)
        {
            button.GetComponent<SpriteRenderer>().color = new Color32(41, 41, 41, 255);
        }
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        FindObjectOfType<DefenderSpawner>().SetSelectedDefender(defenderPrerab);
    }
}
