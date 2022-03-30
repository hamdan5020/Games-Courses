using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdventureGame : MonoBehaviour
{
    [SerializeField] Text textCmpnt;
    [SerializeField] State startState;
    State crntState;
    // Start is called before the first frame update
    void Start()

    {
        crntState = startState;
        textCmpnt.text = crntState.GetStoryText();
    }

    // Update is called once per frame
    void Update()
    {
        ManageState();
    }

    private void ManageState()
    {
        var nextStates = crntState.getNextStates();
        if (Input.GetKeyDown(KeyCode.Alpha1)) 
        {
            crntState = nextStates[0];
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            crntState = nextStates[1];
        }else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            crntState = nextStates[2];
        }
    }
}
