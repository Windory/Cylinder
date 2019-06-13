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

        model.SetMaxCrank(p_controller.GetBorneLim());
    }

    public void Crank()
    {
        if (model.IsLimitPoint() && !GameManager.Instance().Proceed(p_controller.GetDents()))
        {
            Debug.Log("Mauvaise combinaison");
            return;
        }
        model.Crank();
        view.UpdateView(model.GetState());
        p_controller.Read();
    }

    public void ReverseCrank()
    {
        if (model.IsBeginning())
        {
            Debug.Log("Max ReverseCrank");
            return;
        }
        model.ReverseCrank();
        view.UpdateView(model.GetState());
        p_controller.ReverseRead();
    }
}
