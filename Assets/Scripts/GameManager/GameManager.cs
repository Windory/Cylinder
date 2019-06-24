using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    private int currentLevel = 0;
    private int nbLevels = 5;
    private int nbDents = 8;
    bool end = false;
    public GameObject[] dents = new GameObject[4];
    
    int[][] soluce;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        soluce = new int[nbLevels][];
        soluce[0] = new int[] { 0, 5, 7, 3, 0, 6, 0, 0 };
        soluce[1] = new int[] { 3, 6, 8, 7, 0, 5, 0, 0 };
        soluce[2] = new int[] { 3, 8, 5, 7, 6, 4, 0, 0 };
        soluce[3] = new int[] { 3, 2, 4, 6, 8, 5, 7, 0 };
        soluce[4] = new int[] { 8, 7, 6, 5, 4, 3, 2, 1 };
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static GameManager Instance()
    {
        return instance;
    }

    public bool Proceed(int[] proposition)
    {
        return proposition.SequenceEqual(soluce[currentLevel]);
        return true;
    }

    public bool IsEnd()
    {
        return end;
    }

    public void NextLevel()
    {
        if (currentLevel < nbLevels - 1)
        {
            Instantiate(dents[currentLevel]);
            ++currentLevel;
        }
        else
            end = true;
    }
}
