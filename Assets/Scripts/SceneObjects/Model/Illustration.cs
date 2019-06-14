using UnityEngine;
using System.Collections;
using System;

public class Illustration : MonoBehaviour
{
    private Vector3 startPosition;
    private float xSize;
    private int nbTrans; // Nombre de translations lors d'un tour complet du cylindre
    private float speed;
    private int pos = 0; // Position actuelle (max = nbTrans)

    // Use this for initialization
    void Start()
    {
        startPosition = transform.position;
        xSize = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Read()
    {
        ++pos;
        float newPosition = pos * speed;
        gameObject.transform.position = startPosition + Vector3.left * newPosition;
    }

    public void ReverseRead()
    {
        --pos;
        float newPosition = pos * speed;
        gameObject.transform.position = startPosition + Vector3.left * newPosition;
    }

    public void SetTrans(int nb)
    {
        nbTrans = nb;
        speed = xSize / nbTrans;
    }
}
