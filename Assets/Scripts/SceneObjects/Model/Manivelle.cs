using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manivelle : MonoBehaviour
{
    private int state = 0; // 0 -> 7
    private int nbCrank = 0;
    private int limitCrank = 10; // Nombre de cranks avant que la manivelle ne se bloque si les dents sont mal placées
    private int maxCrank = 0;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Crank()
    {
        if (state == 7)
            state = 0;
        else
            state += 1;

        if (nbCrank == maxCrank)
            nbCrank = 0;
        else
            ++nbCrank;
    }

    public void ReverseCrank()
    {
        if (state == 0)
            state = 7;
        else
            state -= 1;

        if (nbCrank == 0)
            nbCrank = maxCrank;
        else
            --nbCrank;
    }

    public int GetState()
    {
        return state;
    }

    public bool IsLimitPoint()
    {
        return nbCrank == limitCrank;
    }

    public bool IsBeginning()
    {
        return nbCrank == 0;
    }

    public void SetMaxCrank(int crank)
    {
        maxCrank = crank;
    }
}
