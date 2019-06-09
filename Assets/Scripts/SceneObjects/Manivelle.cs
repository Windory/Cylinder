using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manivelle : MonoBehaviour
{
    private bool isActivate;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Si l'utilisateur clic sur la manivelle
        //   Crank();
        // Si l'utilisateur retire le clic de la manivelle
        //   Stop();
    }

    public void Crank()
    {
        isActivate = true;
    }

    public void Stop()
    {
        isActivate = false;
    }

    public bool IsActivate()
    {
        return isActivate;
    }
}
