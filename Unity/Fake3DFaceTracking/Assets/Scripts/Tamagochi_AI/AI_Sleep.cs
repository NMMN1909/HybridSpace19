using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Sleep : MonoBehaviour {

    //Reference
    private AI_Variables stats;
    private AI_Controller controller;
    private AI_StateMachine stateMachine;

	// Use this for initialization
	void Start () {
        stats = GetComponent<AI_Variables>();
        controller = GetComponent<AI_Controller>();
        stateMachine = GetComponent<AI_StateMachine>();
	}

    private void Update()
    {
        //if tick on screen,
        //- attention += x

        //if attention == x.amount
        //- wake
        //= happiness -= x.amount

        //if light is on
        //- cant sleep

        //if sleeping and light on
        //- upset
    }

    public void Sleep()
    {
        if (!stats.isAwake)
        {
            StopAllCoroutines();
            stats.energy += .1f;
        }

        if (stats.energy > stats.energyToWake && !stats.isAwake)
        {
            stateMachine.State = AI_StateMachine.state.Wake;
        }
    }
}
