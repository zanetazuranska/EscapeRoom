using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SceneFade : MonoBehaviour
{
    public void Out()
    {
        GetComponent<Animator>().SetBool("Out", false);

        OnOutAnimComplete.Invoke();
    }

    public void In()
    {
        GetComponent<Animator>().SetBool("In", false);

        OnInAnimComplete.Invoke();
    }

    public UnityEvent OnOutAnimComplete = new UnityEvent();
    public UnityEvent OnInAnimComplete = new UnityEvent();


}
