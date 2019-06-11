using UnityEngine;
using System.Collections;
using System;

public class EmplacementDent : MonoBehaviour
{
    private Dent dent;
    private Renderer r;

    // Drag and Drop
    private Color activeDragColor = Color.cyan;
    private Color mouseOverColor = Color.blue;
    private Color originalColor = Color.white;
    private bool dragging = false;
    private bool selected = false;
    private float distance;
    private Vector3 posDent;

    // Use this for initialization
    void Awake()
    {
        r = GetComponent<Renderer>();
        posDent = GetComponent<BoxCollider2D>().bounds.center;
        gameObject.layer = 2;
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
            r.material.color = mouseOverColor;
        }
    }

    void OnMouseExit()
    {
        if (dragging)
        {
            selected = false;
            r.material.color = activeDragColor;
        }
    }

    public void ActiveDrag()
    {
        dragging = true;
        gameObject.layer = 0;
        r.material.color = activeDragColor;
        r.sortingOrder = 2;
    }

    public void DisableDrag()
    {
        dragging = false;
        gameObject.layer = 2;
        r.material.color = originalColor;
        r.sortingOrder = 1;
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
