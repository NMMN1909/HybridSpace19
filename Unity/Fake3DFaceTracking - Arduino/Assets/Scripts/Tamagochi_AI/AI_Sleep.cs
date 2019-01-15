using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Sleep : MonoBehaviour {

    //Reference
    private AI_Variables stats;
    private AI_Controller controller;
    private AI_StateMachine stateMachine;

    public bool disturbedBool;

    // Use this for initialization
    void Start () {
        stats = GetComponent<AI_Variables>();
        controller = GetComponent<AI_Controller>();
        stateMachine = GetComponent<AI_StateMachine>();
        disturbedBool = false;
    }

    public void Sleep()
    {
        if (stats.attention > 80 && stateMachine.State == AI_StateMachine.state.Sleep)
        {
            stateMachine.State = AI_StateMachine.state.Wake;
            disturbedBool = true;
        }
        else if (stats.energy > stats.energyToWake && !stats.isAwake)
        {
            stateMachine.State = AI_StateMachine.state.Wake;
        }
    }
}
