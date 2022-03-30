using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

public class Level : MonoBehaviour
{
    [SerializeField] int blocks;

    public void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    SceneLoader sceneLoader;
    public void CountBlocks()
    {
        blocks++;
    }

    public void BlockDestroyed()
    {
        blocks--;
        if (blocks <= 0)
        {
            sceneLoader.LoadNextScene();
        }
    }
}
