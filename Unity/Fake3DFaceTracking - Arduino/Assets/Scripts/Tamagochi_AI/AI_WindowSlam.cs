using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_WindowSlam : MonoBehaviour {

    //Reference
    private AI_Variables stats;
    private AI_Controller controller;
    private AI_StateMachine stateMachine;
    private AI_Aware aware;
    private AI_Interact interact;
    private AI_Sleep sleep;

    public Transform head;
    public Transform tamagochiHead;
    public Transform windowRot1;
    public Transform windowRot2;

    private bool canWindowSlam;
    private bool isWindowSlam;
    public int upsetCounter;

    // Use this for initialization
    void Start () {
        stats = GetComponent<AI_Variables>();
        controller = GetComponent<AI_Controller>();
        stateMachine = GetComponent<AI_StateMachine>();
        aware = GetComponent<AI_Aware>();
        interact = GetComponent<AI_Interact>();
        sleep = GetComponent<AI_Sleep>();
        canWindowSlam = true;
        upsetCounter = 0;
    }
	
	// Update is called once per frame
	void Update () {

        if (isWindowSlam)
        {
            Vector3 lookPos = head.transform.position - transform.position;
            lookPos.y = 0;
            Quaternion rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(this.transform.rotation, rotation, Time.deltaTime * 2f);
            aware.isAware = true;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                StopAllCoroutines();
                stateMachine.State = AI_StateMachine.state.Notice;
            }
        }

        if (upsetCounter >= 2)
            upsetCounter = 0;

        if (upsetCounter == 0)
            stats.upsetBool = true;
        else
            stats.upsetBool = false;
    }

    public void WindowSlam()
    {
        if (canWindowSlam)
        {
            StopAllCoroutines();
            StartCoroutine(WindowSlamCycle());
            canWindowSlam = false;
        }
    }

    IEnumerator WindowSlamCycle()
    {
        if (sleep.disturbedBool)
        {
            isWindowSlam = true;
            yield return new WaitForSeconds(stats.windowSlamDuration);
            upsetCounter += 1;
            stateMachine.State = AI_StateMachine.state.Sleep;
            yield return new WaitForSeconds(.1f);
            sleep.disturbedBool = false;
            isWindowSlam = false;
            aware.isAware = false;
            canWindowSlam = true;
        }
        else
        {
            isWindowSlam = true;
            yield return new WaitForSeconds(stats.windowSlamDuration);
            upsetCounter += 1;
            stateMachine.State = AI_StateMachine.state.Notice;
            yield return new WaitForSeconds(.1f);
            isWindowSlam = false;
            aware.isAware = false;
            canWindowSlam = true;
        }

    }
}
