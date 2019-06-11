using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manivelle : MonoBehaviour
{
    //temp
    private int active = 0; // 0 -> 8

    private int state = 0; // 0 -> 7


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

        //temp
        if (active == 8)
            active = 0;
        else
            active += 1;
    }

    public void ReverseCrank()
    {
        if (state == 0)
            state = 7;
        else
            state -= 1;

        //temp
        if (active == 0)
            active = 8;
        else
            active -= 1;
    }

    //temp
    public int GetActive()
    {
        return active;
    }

    public int GetState()
    {
        return state;
    }
}
