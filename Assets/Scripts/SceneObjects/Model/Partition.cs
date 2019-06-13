using UnityEngine;
using System.Collections;
using System;
using System.IO;

public class Partition : MonoBehaviour
{
    public Ligne[] ligne_list = new Ligne[8];
    private bool[][] partition = new bool[8][];

    // Partie visible de la partition
    private int borneInf = 0;
    private int borneSup = 8;
    private int borneLim = 143;


    private void Awake()
    {
        for (int i = 0; i < 8; ++i)
        {
            partition[i] = new bool[borneLim + 1];
        }
        Load("Assets/Resources/Partition.txt");
    }

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    private void Load(string filename)
    {
        StreamReader file = new StreamReader(filename);
        string line;

        int j = 0;
        while ((line = file.ReadLine()) != null && j < 144)
        {
            string[] noteList = line.Split('|');

            int i = 7;
            foreach (string note in noteList)
            {
                if (note == "")
                    continue;

                bool isNote;
                if (note == "*")
                    isNote = true;
                else
                    isNote = false;
                partition[i][j] = isNote;
                --i;
            }
            ++j;
        }
    }

    public void Read()
    {
        if (borneSup < borneLim)
            ++borneSup;
        else
            borneSup = 0;

        if (borneInf < borneLim)
            ++borneInf;
        else
            borneInf = 0;
    }

    public void ReverseRead()
    {
        if (borneInf > 0)
            --borneInf;
        else
            borneInf = borneLim;

        if (borneSup > 0)
            --borneSup;
        else
            borneSup = borneLim;
    }

    public Ligne[] GetLigneList()
    {
        return ligne_list;
    }

    public bool[][] GetPartition()
    {
        return partition;
    }

    public int GetBorneInf()
    {
        return borneInf;
    }

    public int GetBorneSup()
    {
        return borneSup;
    }

    public int GetBorneLim()
    {
        return borneLim;
    }
}
