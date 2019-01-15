using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_ReadCard : MonoBehaviour {

    //Reference
    private AI_Variables stats;
    private AI_Controller controller;
    private AI_StateMachine stateMachine;
    private AI_Aware aware;
    private AI_Interact interact;
    public scr_Card card;

    // Use this for initialization
    void Start () {
        stats = GetComponent<AI_Variables>();
        controller = GetComponent<AI_Controller>();
        stateMachine = GetComponent<AI_StateMachine>();
        interact = GetComponent<AI_Interact>();
        aware = GetComponent<AI_Aware>();
    }
	
	// Update is called once per frame
	void Update () {
        if (controller.stopAllCoroutines)
            StartCoroutine(StopCoroutines());

        if (stateMachine.State != AI_StateMachine.state.ReadCard)
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
