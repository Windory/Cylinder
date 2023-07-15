using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Echap : MonoBehaviour
{
    void Start()
    {
        Application.targetFrameRate = 30;
    }


    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
