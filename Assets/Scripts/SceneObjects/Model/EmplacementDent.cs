using UnityEngine;
using System.Collections;
using System;

public class EmplacementDent : MonoBehaviour
{
    private Dent dent;

    // Drag and Drop
    private Color activeDragColor = Color.white;
    private Color mouseOverColor = Color.blue;
    private Color originalColor = Color.yellow;
    private bool dragging = false;
    private bool selected = false;
    private float distance;
    private Vector3 posDent;

    // Use this for initialization
    void Start()
    {
        posDent = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseEnter()
    {
        if (dragging)
        {
            selected = true;
            GetComponent<Renderer>().material.color = mouseOverColor;
        }
    }

    void OnMouseExit()
    {
        if (dragging)
        {
            selected = false;
            GetComponent<Renderer>().material.color = activeDragColor;
        }
    }

    public void ActiveDrag()
    {
        dragging = true;
        GetComponent<Renderer>().material.color = activeDragColor;
    }

    public void DisableDrag()
    {
        dragging = false;
        GetComponent<Renderer>().material.color = originalColor;
    }

    public void SwitchDent(EmplacementDent empTarget)
    {
        empTarget.Unselect();
        Dent temp = empTarget.GetDent();
        empTarget.SetDent(dent);
        dent.SetEmp(empTarget);
        SetDent(temp);
        if (temp != null)
            temp.SetEmp(this);
    }

    public void Unselect()
    {
        selected = false;
    }

    public bool IsSelected()
    {
        return selected;
    }

    public void SetDent(Dent dent)
    {
        this.dent = dent;
    }

    public Dent GetDent()
    {
        return dent;
    }

    public Vector3 GetPos()
    {
        return posDent;
    }
}
