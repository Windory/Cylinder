using UnityEngine;
using System.Collections;
using System;

public class ManivelleView : MonoBehaviour
{
    private ManivelleController controller;
    private SpriteRenderer sr;
    private BoxCollider2D bc;

    public Sprite[] spriteList = new Sprite[8];
    public Sprite[] spriteListMan = new Sprite[8];
    public Vector2[] centerList = new Vector2[8];
    int state = 0; // 0 -> 8

    // Drag
    bool autoMode = true;
    bool move = true;
    Color originalColor = Color.white;
    Color mouseOverColor = Color.cyan;
    bool dragging;
    float distance;
    Vector3 rotationPoint;

    private void Awake()
    {
        controller = GetComponent<ManivelleController>();
        sr = GetComponent<SpriteRenderer>();
        bc = GetComponent<BoxCollider2D>();
    }

    // Use this for initialization
    void Start()
    {
        rotationPoint = GameObject.Find("RotationPoint").transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!autoMode && dragging && move)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector2 rayPoint = ray.GetPoint(distance);
            Vector2 localPos = gameObject.transform.position;
            float currentDist = Vector2.Distance(rayPoint, centerList[state] + localPos);
            
            if (currentDist > Vector2.Distance(rayPoint, centerList[Next()] + localPos))
                controller.Crank();
            else if (currentDist > Vector2.Distance(rayPoint, centerList[Previous()] + localPos))
                controller.ReverseCrank();
        }
    }

    private int Next()
    {
        if (state == 7)
            return 0;
        return state + 1;
    }

    private int Previous()
    {
        if (state == 0)
            return 7;
        return state - 1;
    }

    public void OnMouseEnter()
    {
        if (move)
            GetComponent<SpriteRenderer>().color = mouseOverColor;
    }

    public void OnMouseExit()
    {
        if (move)
            GetComponent<SpriteRenderer>().color = originalColor;
    }

    public void OnMouseDown()
    {
        if (move)
        {
            if (!autoMode)
            {
                dragging = true;
                distance = Vector3.Distance(transform.position, Camera.main.transform.position);
            }
            else
            {
                GetComponent<SpriteRenderer>().color = originalColor;
                controller.AutoCrank();
            }
        }
    }

    public void OnMouseUp()
    {
        if (move && !autoMode)
            dragging = false;
    }

    public void SetMove(bool b)
    {
        move = b;
        dragging = false;
    }

    public void UpdateView(int state)
    {
        this.state = state;
        if (autoMode)
            sr.sprite = spriteList[state];
        else
            sr.sprite = spriteListMan[state];
        bc.offset = centerList[state];
    }

    public void SwitchMode()
    {
        autoMode = !autoMode;
    }
}