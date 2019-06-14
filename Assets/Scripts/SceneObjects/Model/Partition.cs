using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;

public class Partition : MonoBehaviour
{
    public Ligne[] ligne_list = new Ligne[8];

    private string filename = "Partition";
    private List<bool>[] partition = new List<bool>[8];

    // Partie visible de la partition
    private int borneInf = 0;
    private int borneSup = 8;
    private int borneLim;


    private void Awake()
    {
        for (int i = 0; i < 8; ++i)
        {
            partition[i] = new List<bool>();
        }
        Load();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    
    private void Load()
    {
        try
        {
            string file = Resources.Load<TextAsset>(filename).ToString();
            string[] array = file.Split('\n');

            int j = 0;
            foreach (string line in array)
            {
                string[] noteList = line.Split('|');

                int i = 7;
                foreach (string note in noteList)
                {
                    if (note == "" || i < 0)
                        continue;

                    bool isNote;
                    if (note == "*")
                        isNote = true;
                    else
                        isNote = false;
                    partition[i].Add(isNote);
                    --i;
                }
                ++j;
            }
            borneLim = j - 1;
        }
        catch (Exception e)
        {
            // Directory not found
            Debug.Log(e.Message);
            Application.Quit();
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

    public List<bool>[] GetPartition()
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

    public int[] GetDents()
    {
        int[] result = new int[8];
        int i = 0;
        foreach (Ligne ligne in ligne_list)
        {
            result[i] = ligne.GetIdDent();
            ++i;
        }
        return result;
    }
}
