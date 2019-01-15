using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Wake : MonoBehaviour {

    private AI_StateMachine stateMachine;
    private AI_Controller controller;
    private AI_Variables stats;

    private bool canWake;

	// Use this for initialization
	void Start () {
        controller = GetComponent<AI_Controller>();
        stateMachine = GetComponent<AI_StateMachine>();
        stats = GetComponent<AI_Variables>();
        canWake = true;
	}

    public void Wake()
    {
        if (canWake)
        {
            StartCoroutine(WakeCycle());
            canWake = false;
        }
    }

    IEnumerator WakeCycle()
    {
        stats.happiness += 40;
        stats.amusement += 40;
        yield return new WaitForSeconds(2f);
        stateMachine.State = AI_StateMachine.state.Notice;
        controller.canNewState = true;
        yield return new WaitForSeconds(.1f);
        stats.isAwake = true;
        canWake = true;
    }
}
