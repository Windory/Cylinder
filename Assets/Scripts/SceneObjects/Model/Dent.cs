using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dent : MonoBehaviour
{
    public EmplacementDent emp;
    public AudioClip sound;
    public int id = 0;

    // Drag and Drop
    private Color mouseOverColor = Color.blue;
    private Color originalColor = Color.yellow;
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
    }

    // Update is called once per frame
    void Update()
    {
        if (dragging)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 rayPoint = ray.GetPoint(distance);
            transform.position = rayPoint;
        }
    }

    public void OnMouseEnter()
    {
        GetComponent<Renderer>().material.color = mouseOverColor;
    }

    public void OnMouseExit()
    {
        GetComponent<Renderer>().material.color = originalColor;
    }

    public void OnMouseDown()
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

    public void OnMouseUp()
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
        AudioSource.PlayClipAtPoint(sound, transform.position);
    }

    public int GetId()
    {
        return id;
    }
}
