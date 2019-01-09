using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Idle : MonoBehaviour {

    //Reference
    private AI_Variables stats;
    private AI_Controller controller;

    private bool canIdle;

    void Start()
    {
        stats = GetComponent<AI_Variables>();
        controller = GetComponent<AI_Controller>();
        canIdle = true;
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
        stats.movDirection = Vector3.zero;
        yield return new WaitForSeconds(stats.idleDuration);
        controller.canNewState = true;
        yield return new WaitForSeconds(.1f);
        canIdle = true;
    }
}
