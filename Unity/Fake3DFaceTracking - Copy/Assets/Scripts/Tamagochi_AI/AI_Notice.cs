using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Notice : MonoBehaviour {

    //Reference
    private AI_Variables stats;
    private AI_Controller controller;
    private AI_StateMachine stateMachine;
    public Transform head;

    private bool canNotice;
    private bool isNotice;

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
	}

    public void Notice()
    {
        if (canNotice)
        {
            StopAllCoroutines();
            StartCoroutine(NoticeCycle());
            stats.attention += Random.Range(10,20);
            canNotice = false;
        }
    }

    public void AwareNoticeCheck()
    {
        //if(stats.attention)
    }

    IEnumerator NoticeCycle()
    {
        //Notice, look at player
        stats.movDirection = Vector3.zero;
        isNotice = true;
        //Play Notice Animation
        yield return new WaitForSeconds(stats.noticeDuration);
        stateMachine.State = AI_StateMachine.state.Default;
        controller.canNewState = true;
        yield return new WaitForSeconds(.1f);
        canNotice = true;
        isNotice = false;
    }
}
