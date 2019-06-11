using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManivelleController : MonoBehaviour
{
    private Manivelle model;
    private ManivelleView view;


    // Start is called before the first frame update
    void Start()
    {
        model = GetComponent<Manivelle>();
        view = GetComponent<ManivelleView>();
    }

    // Update is called once per frame
    void Update()
    {
        // Si l'utilisateur clic sur la manivelle
        //   Crank();
        // Si l'utilisateur retire le clic de la manivelle
        //   Stop();
    }

    public void Crank()
    {
        Debug.Log("Crank");
        model.Crank();
        view.UpdateView(model.GetState(), model.GetActive()); //temp
    }

    public void ReverseCrank()
    {
        Debug.Log("ReverseCrank");
        model.ReverseCrank();
        view.UpdateView(model.GetState(), model.GetActive()); //temp
    }
}
