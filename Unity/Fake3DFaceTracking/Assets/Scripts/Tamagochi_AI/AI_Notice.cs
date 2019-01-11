using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Notice : MonoBehaviour {

    //Reference
    private AI_Variables stats;
    private AI_Controller controller;
    private AI_StateMachine stateMachine;
    public Transform head;

    public bool canNotice;
    public bool isNotice;

	// Use this for initialization
	void Start () {
        stats = GetComponent<AI_Variables>();
        controller = GetComponent<AI_Controller>();
        stateMachine = GetComponent<AI_StateMachine>();
        canNotice = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (isNotice)
        {
            Vector3 lookPos = head.transform.position - transform.position;
            lookPos.y = 0;
            Quaternion rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(this.transform.rotation, rotation, Time.deltaTime *2f);
        }

        if (controller.stopAllCoroutines)
            StartCoroutine(StopCoroutines());
        
        if(stateMachine.State != AI_StateMachine.state.Notice)
        {
            canNotice = true;
            isNotice = false;
            StopAllCoroutines();
        }
	}

    public void Notice()
    {
        if (canNotice)
        {
            controller.stopAllCoroutines = false;
            StartCoroutine(NoticeCycle());
            stats.attention += Random.Range(10,20);
            canNotice = false;
        }
    }

    public IEnumerator NoticeCycle()
    {
        //Notice, look at player
        isNotice = true;
        yield return new WaitForSeconds(stats.noticeDuration);
        StartCoroutine(controller.NewState());

    }

    private IEnumerator StopCoroutines()
    {
        StartCoroutine(controller.NewState());
        controller.stopAllCoroutines = false;
        yield return new WaitForSeconds(.01f);
        StopAllCoroutines();
    }




}
