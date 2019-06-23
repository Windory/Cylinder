using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dent : MonoBehaviour
{
    public EmplacementDent emp;
    public AudioClip sound;
    public int id = 0;

    private AudioSource source;

    // Drag and Drop
    private static bool move = true;
    private Color mouseOverColor = Color.yellow;
    private Color originalColor = Color.white;
    private bool dragging = false;
    private float distance;
    private Vector3 initialPos;

    // Start is called before the first frame update
    void Start()
    {
        if (emp == null)
        {
            emp = GameObject.Find("EmplacementDefaut").GetComponent<EmplacementDent>();
        }
        NewPosition(emp.GetPos());
        emp.SetDent(this);
        source = GameObject.Find("Main Camera").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dragging && move)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 rayPoint = ray.GetPoint(distance);
            transform.position = rayPoint;
        }
    }

    public static void SetMove(bool b)
    {
        move = b;
    }

    public void OnMouseEnter()
    {
        if (move)
            GetComponent<Renderer>().material.color = mouseOverColor;
    }

    public void OnMouseExit()
    {
        if (move)
            GetComponent<Renderer>().material.color = originalColor;
    }

    public void OnMouseDown()
    {
        if (move)
        {
            dragging = true;
            distance = Vector3.Distance(transform.position, Camera.main.transform.position);

            foreach (EmplacementDent empDent in FindObjectsOfType<EmplacementDent>())
            {
                empDent.ActiveDrag();
            }

            foreach (Dent dent in FindObjectsOfType<Dent>())
            {
                dent.gameObject.layer = 2;
            }
        }
    }

    public void OnMouseUp()
    {
        if (move)
        {
            dragging = false;

            bool isSwitch = false;
            foreach (EmplacementDent empDent in FindObjectsOfType<EmplacementDent>())
            {
                empDent.DisableDrag();
                if (empDent.IsSelected())
                {
                    SwitchEmp(empDent);
                    isSwitch = true;
                }
            }

            if (!isSwitch)
            {
                InitialPosition();
            }

            foreach (Dent dent in FindObjectsOfType<Dent>())
            {
                dent.gameObject.layer = 0;
            }
        }
    }

    public void InitialPosition()
    {
        gameObject.transform.position = initialPos;
    }

    public void NewPosition(Vector3 pos)
    {
        gameObject.transform.position = pos;
        initialPos = pos;
    }

    public void SwitchEmp(EmplacementDent emp2)
    {
        emp.SwitchDent(emp2);
    }

    public void SetEmp(EmplacementDent emp)
    {
        this.emp = emp;
        NewPosition(emp.GetPos());
    }

    public EmplacementDent GetEmp()
    {
        return emp;
    }

    public void Play()
    {
        source.PlayOneShot(sound, 1);
    }

    public int GetId()
    {
        return id;
    }
}
