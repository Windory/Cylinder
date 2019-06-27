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
    int wait_final = 120;
    int final_img = 1;
    bool final = false;
    bool end = false;
    bool pause = false;

    int wait = 0;
    int waitingTime = 8; // Nombre de frames pendant lesquels la manivelle ne réagit plus après avoir été actionnée

    bool reset = false;
    bool auto = false;
    bool autoMode = true;

    public AudioClip badAnswer;
    private AudioSource source;

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
        source = GameObject.Find("Main Camera").GetComponent<AudioSource>();
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
        else if (!final && GameManager.Instance().IsEnd())
        {
            final = true;
            Dent.SetMove(false);
            view.SetMove(false);
        }
        else if (final && wait_final != 0)
        {
            --wait_final;
        }
        else if (final && final_img != 0 && !end)
        {
            ReverseCrank();
        }
        else if (!end && final_img == 0)
        {
            end = true;
            Dent.SetMove(true);
            view.SetMove(true);
        }
    }

    private IEnumerator waitForError()
    {
        pause = true;
        source.Stop();
        source.PlayOneShot(badAnswer, 1);
        while (source.isPlaying)
        {
            yield return null;
        }
        pause = false;
    }

    public void Crank()
    {
        if (wait == 0 && !pause)
        {
            if (model.IsBeginning() && !end)
            {
                Dent.SetMove(false);
                if (autoMode)
                    view.SetMove(false);
            }
            else if (model.IsLimitPoint() && !end)
            {
                if (!GameManager.Instance().Proceed(p_controller.GetDents()))
                {
                    StartCoroutine(waitForError());
                    ResetCrank();
                    return;
                }
            }

            model.Crank();
            view.UpdateView(model.GetState());
            p_controller.Read();

            if (!end)
                illustration.Read();

            if (model.IsBeginning())
            {
                GameManager.Instance().NextLevel();
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
        if (wait == 0 && !pause)
        {
            if (model.IsBeginning() && !end)
            {
                if (!final || end)
                    return;
            }

            model.ReverseCrank();
            view.UpdateView(model.GetState());
            p_controller.ReverseRead();

            if (!end)
            {
                if (!final)
                    illustration.ReverseRead();
                else
                    illustration.FastReverseRead();
            }

            if (model.IsBeginning())
            {
                if (!final)
                {
                    Dent.SetMove(true);
                    view.SetMove(true);
                    reset = false;
                    auto = false;
                }
                else if (!end && final_img != 0)
                    --final_img;
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

        int speed = 10 - (int)slider.value;
        ChangeWait(speed);
    }

    public void ChangeWait(int wait)
    {
        waitingTime = wait;
        illustration.SetWait(waitingTime);
    }
}
