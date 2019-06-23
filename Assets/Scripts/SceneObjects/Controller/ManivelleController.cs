using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManivelleController : MonoBehaviour
{
    private Manivelle model;
    private ManivelleView view;
    private PartitionController p_controller;
    private Illustration illustration;

    int wait = 0;
    int waitingTime = 8; // Nombre de frames pendant lesquels la manivelle ne réagit plus après avoir été actionnée
    bool reset = false;
    bool auto = false;
    bool autoMode = true;

    // Start is called before the first frame update
    void Start()
    {
        model = GetComponent<Manivelle>();
        view = GetComponent<ManivelleView>();
        p_controller = GameObject.FindObjectOfType<PartitionController>();
        illustration = GameObject.FindObjectOfType<Illustration>();
        illustration.SetTrans(p_controller.GetBorneLim());
        illustration.SetWait(waitingTime);

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
        else if (auto)
        {
            Crank();
        }
    }

    public void Crank()
    {
        if (wait == 0)
        {
            if (model.IsBeginning())
            {
                Dent.SetMove(false);
                if (autoMode)
                    view.SetMove(false);
            }
            else if (model.IsLimitPoint())
            {
                if (!GameManager.Instance().Proceed(p_controller.GetDents()))
                {
                    ResetCrank();
                    return;
                }
                else
                {

                }
            }

            model.Crank();
            view.UpdateView(model.GetState());
            p_controller.Read();
            illustration.Read();

            if (model.IsBeginning())
            {
                Dent.SetMove(true);
                auto = false;
                if (autoMode)
                    view.SetMove(true);
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
            illustration.ReverseRead();

            if (model.IsBeginning())
            {
                Dent.SetMove(true);
                view.SetMove(true);
                reset = false;
                auto = false;
            }

            wait = waitingTime;
        }
    }

    public void ResetCrank()
    {
        view.SetMove(false);
        reset = true;
    }

    public void AutoCrank()
    {
        view.SetMove(false);
        auto = true;
    }

    public void SwitchMode()
    {
        autoMode = !autoMode;
        view.SwitchMode();
    }

    public void SetSpeed(Slider slider)
    {
        if (illustration == null)
            return;
        waitingTime = 10 - (int)slider.value;
        illustration.SetWait(waitingTime);
    }
}
