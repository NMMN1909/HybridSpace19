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

    public Transform head;
    public Transform tamagochiHead;
    public Transform windowRot1;
    public Transform windowRot2;

    private bool canWindowSlam;
    private bool isWindowSlam;

	// Use this for initialization
	void Start () {
        stats = GetComponent<AI_Variables>();
        controller = GetComponent<AI_Controller>();
        stateMachine = GetComponent<AI_StateMachine>();
        aware = GetComponent<AI_Aware>();
        interact = GetComponent<AI_Interact>();
        canWindowSlam = true;
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
        }
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
        isWindowSlam = true;
        yield return new WaitForSeconds(stats.windowSlamDuration);
        stateMachine.State = AI_StateMachine.state.Notice;
        yield return new WaitForSeconds(.1f);
        isWindowSlam = false;
        aware.isAware = false;
        canWindowSlam = true;
    }
}
