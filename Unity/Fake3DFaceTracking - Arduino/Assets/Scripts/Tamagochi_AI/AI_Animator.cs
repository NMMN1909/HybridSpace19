using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Animator : MonoBehaviour {

    private Animator animController;
    private AI_StateMachine stateMachine;
    private AI_Idle idle;
    private AI_Roaming roaming;
    private AI_Upset upset;
    private AI_Respond respond;
    public scr_Card card;

    private bool doOnce;

	// Use this for initialization
	void Start () {
        animController = GetComponent<Animator>();
        stateMachine = GetComponent<AI_StateMachine>();
        idle = GetComponent<AI_Idle>();
        roaming = GetComponent<AI_Roaming>();
        upset = GetComponent<AI_Upset>();
        respond = GetComponent<AI_Respond>();
        doOnce = true;
	}
	
	// Update is called once per frame
	void Update () {
        if(stateMachine.State == AI_StateMachine.state.Sleep)
            animController.SetBool("isSleeping", true);
        else if(stateMachine.State == AI_StateMachine.state.Wake)
            animController.SetBool("isWaking", true);
        else if (stateMachine.State == AI_StateMachine.state.Notice)
            animController.SetBool("isNotice", true);
        else if (stateMachine.State == AI_StateMachine.state.ReadCard)
            animController.SetBool("isReadingCard", true);
        else if (stateMachine.State == AI_StateMachine.state.Interact)
            animController.SetBool("isIdle", true);
        else if(stateMachine.State == AI_StateMachine.state.WindowSlam)
            animController.SetBool("isWindowSlam", true);
        else if (roaming.canMovForward || stateMachine.State == AI_StateMachine.state.Respond || stateMachine.State == AI_StateMachine.state.Upset || stateMachine.State == AI_StateMachine.state.Playing)
            animController.SetBool("isRoaming", true);


        if (stateMachine.State != AI_StateMachine.state.Sleep)
            animController.SetBool("isSleeping", false);
        if (stateMachine.State != AI_StateMachine.state.Wake)
            animController.SetBool("isWaking", false);
        if (stateMachine.State != AI_StateMachine.state.WindowSlam)
            animController.SetBool("isWindowSlam", false);
        if (stateMachine.State != AI_StateMachine.state.Notice)
            animController.SetBool("isNotice", false);
        if (stateMachine.State != AI_StateMachine.state.Interact)
            animController.SetBool("isIdle", false);
        if (!roaming.canMovForward && stateMachine.State != AI_StateMachine.state.Respond && stateMachine.State != AI_StateMachine.state.Upset && stateMachine.State != AI_StateMachine.state.Playing)
            animController.SetBool("isRoaming", false);
        if (stateMachine.State != AI_StateMachine.state.ReadCard)
            animController.SetBool("isReadingCard", false);
    }
}
