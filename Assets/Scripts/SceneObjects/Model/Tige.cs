using UnityEngine;
using System.Collections;

public class Tige : MonoBehaviour
{
    private bool active = false;

    public void SetActive(bool b)
    {
        active = b;
        if (b)
            gameObject.SetActive(true);
        else
            gameObject.SetActive(false);
    }
}
