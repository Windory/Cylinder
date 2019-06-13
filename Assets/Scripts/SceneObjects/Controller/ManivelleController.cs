using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManivelleController : MonoBehaviour
{
    private Manivelle model;
    private ManivelleView view;
    private PartitionController p_controller;

    int wait = 0;
    int waitingTime = 5; // Nombre de frames pendant lesquels la manivelle ne réagit plus après avoir été actionnée
    bool reset = false;

    // Start is called before the first frame update
    void Start()
    {
        model = GetComponent<Manivelle>();
        view = GetComponent<ManivelleView>();
        p_controller = GameObject.Find("Partition").GetComponent<PartitionController>();

        model.SetMaxCrank(p_controller.GetBorneLim());
    }

    private void Update()
    {
        if (wait > 0)
            --wait;
        if (wait < 0)
            wait = 0;
        if (reset)
        {
            ReverseCrank();
        }
    }

    public void Crank()
    {
        if (wait == 0)
        {
            if (model.IsBeginning())
            {
                Dent.SetMove(false);
            }
            else if (model.IsLimitPoint() && !GameManager.Instance().Proceed(p_controller.GetDents()))
            {
                ResetManivelle();
                return;
            }

            model.Crank();
            view.UpdateView(model.GetState());
            p_controller.Read();

            if (model.IsBeginning())
            {
                Dent.SetMove(true);
            }

            wait = waitingTime;
        }
    }

    public void ReverseCrank()
    {
        if (wait == 0)
        {
            if (model.IsBeginning())
            {
                Debug.Log("Max ReverseCrank");
                return;
            }

            model.ReverseCrank();
            view.UpdateView(model.GetState());
            p_controller.ReverseRead();

            if (model.IsBeginning())
            {
                Dent.SetMove(true);
                view.SetMove(true);
                reset = false;
            }

            wait = waitingTime;
        }
    }

    public void ResetManivelle()
    {
        view.SetMove(false);
        reset = true;
    }
}
