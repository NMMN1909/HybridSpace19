using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Respond : MonoBehaviour {

    //Reference
    private AI_Variables stats;
    private AI_Controller controller;
    private AI_StateMachine stateMachine;
    private AI_Aware aware;
    private AI_Interact interact;
    public Transform head;
    public Transform tamagochiHead;
    public Transform interactionManager;

    public bool canRespond;
    public bool isRespond;

    //Animations
    //public Animation anim_Bounce;

    // Use this for initialization
    void Start () {
        stats = GetComponent<AI_Variables>();
        controller = GetComponent<AI_Controller>();
        stateMachine = GetComponent<AI_StateMachine>();
        aware = GetComponent<AI_Aware>();
        interact = GetComponent<AI_Interact>();
        canRespond = true;
    }
	
	// Update is called once per frame
	void Update () {

        if (controller.stopAllCoroutines)
            StartCoroutine(StopCoroutines());

        if (stateMachine.State != AI_StateMachine.state.Respond)
        {
            canRespond = true;
            isRespond = false;
            StopAllCoroutines();
        }

        if (isRespond && stats.attention >= 40 && stateMachine.State == AI_StateMachine.state.Respond)
        {
            //anim_Bounce.Play("Creature_Bounce");

            Vector3 lookPos = interactionManager.transform.position - transform.position;
            lookPos.y = 0;
            Quaternion rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(this.transform.rotation, rotation, Time.deltaTime * 2f);
            transform.Translate(Vector3.forward * stats.movSpeed * Time.deltaTime);
            aware.isAware = true;
        }
        else if(isRespond && stats.attention < 40 && stateMachine.State == AI_StateMachine.state.Respond)
            StartCoroutine(StateDelay());
        else if(!isRespond && stateMachine.State != AI_StateMachine.state.Respond && !aware.isAware && !interact.isInteract)
            tamagochiHead.transform.rotation = Quaternion.Slerp(tamagochiHead.transform.rotation, Quaternion.LookRotation(this.transform.forward), .5f);

        var dist = Vector3.Distance(transform.position, interactionManager.transform.position);
        if (dist <= 1 && stateMachine.State == AI_StateMachine.state.Respond)
            stateMachine.State = AI_StateMachine.state.Interact;
    }

    public void Respond()
    {
        if (canRespond)
        {
            StartCoroutine(RespondCycle());
            stats.attention += Random.Range(10, 30);
            canRespond = false;
        }
    }

    IEnumerator RespondCycle()
    {
        isRespond = true;
        yield return new WaitForSeconds(0);
    }

    IEnumerator StateDelay()
    {
        StartCoroutine(controller.NewState());
        aware.isAware = false;
        isRespond = false;
        canRespond = true;
        yield return new WaitForSeconds(.1f);
        StopAllCoroutines();
    }

    private IEnumerator StopCoroutines()
    {
        StartCoroutine(controller.NewState());
        controller.stopAllCoroutines = false;
        yield return new WaitForSeconds(.01f);
        StopAllCoroutines();
    }
}
