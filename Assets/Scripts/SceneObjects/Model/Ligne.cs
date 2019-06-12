using UnityEngine;
using System.Collections.Generic;
using System;

public class Ligne : MonoBehaviour
{
    public Tige[] tigeList = new Tige[9];
    public EmplacementDent emp;

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
        throw new NotImplementedException();
    }

    public void Update(bool[] partition, int bornInf, int bornSup, int bornLim)
    {
        if (bornInf > bornSup)
        {
            for (int j = bornSup; j >= 0; --j)
            {
                tigeList[bornSup - j].SetActive(partition[j]);
            }
            for (int j = bornLim; j >= bornInf; --j)
            {
                tigeList[bornLim - j + bornSup + 1].SetActive(partition[j]);
            }
        }

        for (int j = bornSup; j >= bornInf; --j)
        {
            tigeList[bornSup - j].SetActive(partition[j]);
        }

        if (partition[bornInf])
        {
            emp.Play();
        }
    }
}
