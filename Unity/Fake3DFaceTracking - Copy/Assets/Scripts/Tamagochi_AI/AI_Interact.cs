﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Interact : MonoBehaviour {

    //Reference
    private AI_Variables stats;
    private AI_Controller controller;
    private AI_StateMachine stateMachine;
    private AI_Aware aware;
    private AI_Interact interact;

    public Transform tamagochiHead;
    public Transform head;

    public bool isInteract;
    private bool canInteract;

	// Use this for initialization
	void Start () {
        stats = GetComponent<AI_Variables>();
        controller = GetComponent<AI_Controller>();
        stateMachine = GetComponent<AI_StateMachine>();
        interact = GetComponent<AI_Interact>();
        aware = GetComponent<AI_Aware>();
        canInteract = true;
    }
	
	// Update is called once per frame
	void Update () {
        if (isInteract && stats.attention > stats.attentionToInteract && stateMachine.State == AI_StateMachine.state.Interact)
        {
            Vector3 lookPos = head.transform.position - transform.position;
            lookPos.y = 0;
            Quaternion rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(this.transform.rotation, rotation, Time.deltaTime * 2f);
            aware.isAware = true;
        }
        else if (isInteract && stats.attention < 40 && stateMachine.State == AI_StateMachine.state.Interact)
        {
            StartCoroutine(StateDelay());
        }
        else if (!isInteract && stateMachine.State != AI_StateMachine.state.Interact && !aware.isAware && stateMachine.State != AI_StateMachine.state.Respond)
            tamagochiHead.transform.rotation = Quaternion.Slerp(tamagochiHead.transform.rotation, Quaternion.LookRotation(this.transform.forward), .5f);
    }

    public void Interact()
    {
        if (canInteract)
        {
            StopAllCoroutines();
            StartCoroutine(InteractCycle());
            stats.attention = Random.Range(70, 100);
            canInteract = false;
        }
    }

    IEnumerator InteractCycle()
    {
        isInteract = true;
        yield return new WaitForSeconds(0);
    }

    IEnumerator StateDelay()
    {
        stateMachine.State = AI_StateMachine.state.Notice;
        controller.canNewState = true;
        yield return new WaitForSeconds(.1f);
        aware.isAware = false;
        isInteract = false;
        canInteract = true;
    }
}
