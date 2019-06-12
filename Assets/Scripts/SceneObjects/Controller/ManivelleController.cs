using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManivelleController : MonoBehaviour
{
    private Manivelle model;
    private ManivelleView view;
    private PartitionController p_controller;


    // Start is called before the first frame update
    void Start()
    {
        model = GetComponent<Manivelle>();
        view = GetComponent<ManivelleView>();
        p_controller = GameObject.Find("Partition").GetComponent<PartitionController>();
    }

    public void Crank()
    {
        model.Crank();
        view.UpdateView(model.GetState());
        p_controller.Read();
    }

    public void ReverseCrank()
    {
        model.ReverseCrank();
        view.UpdateView(model.GetState());
        p_controller.ReverseRead();
    }
}
