using UnityEngine;
using System.Collections;
using System;

public class Illustration : MonoBehaviour
{
    private Vector3 startPosition; // Start position of the current translation
    private Vector3 endPosition; // End position of the current translation
    private float xSize;
    private int nbTrans; // Nombre de translations lors d'un tour complet du cylindre
    private float speed;
    private int pos = 0; // Position actuelle (max = nbTrans)
    private int remaining = 0; // Remaining translations
    private int waitingTime;

    // Use this for initialization
    void Awake()
    {
        startPosition = transform.position;
        xSize = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (remaining > 0)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, (float)(waitingTime - remaining) / waitingTime);
            --remaining;
        }
        else if (remaining < 0)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, (float)(waitingTime + remaining) / waitingTime);
            ++remaining;
        }
    }

    public void Read()
    {
        ++pos;
        startPosition = endPosition;
        endPosition = startPosition + Vector3.left * speed;
        remaining = waitingTime;
    }

    public void ReverseRead()
    {
        --pos;
        startPosition = endPosition;
        endPosition = startPosition + Vector3.right * speed;
        remaining = -waitingTime;
    }

    public void FastReverseRead()
    {
        startPosition = endPosition;
        endPosition = startPosition + Vector3.right * speed * 6f;
        remaining = -waitingTime;
    }

    public void SetTrans(int nb)
    {
        nbTrans = nb;
        speed = (xSize * 3) / (nbTrans + 1);
    }

    public void SetWait(int wait)
    {
        waitingTime = wait;
    }
}
