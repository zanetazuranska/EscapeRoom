using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationFlowController : MonoBehaviour
{

    private ER.StateMachine _stateMachine = new ER.StateMachine();

    void Start()
    {
        _stateMachine.Start();
    }

    void Update()
    {
        _stateMachine.Update();
    }
}
