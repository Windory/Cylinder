using UnityEngine;
using System.Collections;

public class ManivelleView : MonoBehaviour
{
    private ManivelleController controller;
    private SpriteRenderer sr;
    private BoxCollider2D bc;

    public Sprite[] spriteList = new Sprite[8];
    public Vector2[] centerList = new Vector2[8];
    int state = 0; // 0 -> 8

    Color mouseOverColor = Color.cyan;
    Color originalColor = Color.gray;
    bool dragging;
    float distance;
    Vector3 rotationPoint;

    // Use this for initialization
    void Start()
    {
        controller = GetComponent<ManivelleController>();
        sr = GetComponent<SpriteRenderer>();
        bc = GetComponent<BoxCollider2D>();
        rotationPoint = GameObject.Find("RotationPoint").transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (dragging)
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
    }

    public void OnMouseUp()
    {
        dragging = false;
    }

    public void UpdateView(int state)
    {
        this.state = state;
        sr.sprite = spriteList[state];
        bc.offset = centerList[state];
    }
}