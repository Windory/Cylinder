using UnityEngine;
using System.Collections;
using System;

public class Partition : MonoBehaviour
{
    private Ligne[] ligne_list = new Ligne[8];


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Read()
    {
        for (int i = 0; i < 8; ++i)
        {
            ligne_list[i].Read();
        }
    }
}
