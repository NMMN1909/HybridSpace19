using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Idle : MonoBehaviour {

    //Reference
    private AI_Variables stats;
    private AI_Controller controller;
    private AI_StateMachine stateMachine;

    private bool canIdle;

    void Start()
    {
        stats = GetComponent<AI_Variables>();
        controller = GetComponent<AI_Controller>();
        stateMachine = GetComponent<AI_StateMachine>();
        canIdle = true;
    }

    private void Update()
    {
        if (controller.stopAllCoroutines)
            StartCoroutine(StopCoroutines());

        if (stateMachine.State != AI_StateMachine.state.Default)
        {
            canIdle = true;
            StopAllCoroutines();
        }
    }

    public void Idle()
    {
        if (canIdle)
        {
            StartCoroutine(IdleCycle());
            canIdle = false;
        }
    }

    IEnumerator IdleCycle()
    {
        //Play Idle Animation
        yield return new WaitForSeconds(stats.idleDuration);
        controller.canNewState = true;
        yield return new WaitForSeconds(.1f);
        canIdle = true;
    }

    private IEnumerator StopCoroutines()
    {
        StartCoroutine(controller.NewState());
        controller.stopAllCoroutines = false;
        yield return new WaitForSeconds(.01f);
        StopAllCoroutines();
    }
}
