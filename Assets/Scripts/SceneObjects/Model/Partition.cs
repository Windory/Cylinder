using UnityEngine;
using System.Collections;
using System;
using System.IO;

public class Partition : MonoBehaviour
{
    private Ligne[] ligne_list = new Ligne[8];
    private bool[,] partition = new bool[144, 8];

    // Partie visible de la partition
    private int borneInf = 0;
    private int borneSup = 16;


    // Use this for initialization
    void Start()
    {
        Load("../Partition.txt");
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    private void Load(string filename)
    {
        StreamReader file = new StreamReader(filename);
        string line;

        int i = 0;
        while ((line = file.ReadLine()) != null && i < 144)
        {
            string[] noteList = line.Split('|');

            int j = 0;
            foreach (string note in noteList)
            {
                bool isNote;
                if (note == "*")
                    isNote = true;
                else
                    isNote = false;
                partition[i, j] = isNote;
                ++j;
            }
            ++i;
        }
    }


    public void Read()
    {
        for (int i = 0; i < 8; ++i)
        {
            ligne_list[i].Read();
        }
    }
}
