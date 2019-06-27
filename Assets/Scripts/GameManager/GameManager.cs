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

    public Dent[] dents = new Dent[4];
    public EmplacementDent[] emps = new EmplacementDent[4];
    public GameObject[] placebos = new GameObject[4];
    public Animator animator;
    
    public AudioClip getDent;
    private AudioSource source;

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

        LoadSoluce();
    }

    // Start is called before the first frame update
    void Start()
    {
        SetDents();
        source = GameObject.Find("Main Camera").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LoadSoluce()
    {
        soluce = new int[nbLevels][];
        soluce[0] = new int[] { 0, 5, 7, 3, 0, 6, 0, 0 };
        soluce[1] = new int[] { 3, 6, 8, 7, 0, 5, 0, 0 };
        soluce[2] = new int[] { 3, 8, 5, 7, 6, 4, 0, 0 };
        soluce[3] = new int[] { 3, 2, 4, 6, 8, 5, 7, 0 };
        soluce[4] = new int[] { 8, 7, 6, 5, 4, 3, 2, 1 };
    }

    private void SetDents()
    {
        for (int i = 0; i < 4; ++i)
        {
            dents[i].gameObject.SetActive(false);
            emps[i].gameObject.SetActive(false);
        }
    }

    public static GameManager Instance()
    {
        return instance;
    }

    public bool Proceed(int[] proposition)
    {
        return proposition.SequenceEqual(soluce[currentLevel]);
    }

    public bool IsEnd()
    {
        return end;
    }

    public void NextLevel()
    {
        if (currentLevel < nbLevels - 1)
        {
            Dent newDent = dents[currentLevel];
            EmplacementDent newEmp = emps[currentLevel];
            GameObject placebo = placebos[currentLevel];

            newDent.gameObject.SetActive(true);
            newEmp.gameObject.SetActive(true);
            placebo.SetActive(false);
            animator.SetTrigger("NewDent");
            source.PlayOneShot(getDent, 1);

            ++currentLevel;
        }
        else
            end = true;
    }
}
