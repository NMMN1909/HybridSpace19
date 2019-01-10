using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Upset : MonoBehaviour {

    //Reference
    private AI_Variables stats;
    private AI_Controller controller;
    private AI_StateMachine stateMachine;
    private AI_Aware aware;
    private AI_Interact interact;

    public Transform windowPos1;
    public Transform windowPos2;
    private int windowRandom;

    private bool isUpset;
    private bool canUpset;

    //Animations
    //public Animation anim_Bounce;

    // Use this for initialization
    void Start () {
        stats = GetComponent<AI_Variables>();
        controller = GetComponent<AI_Controller>();
        stateMachine = GetComponent<AI_StateMachine>();
        interact = GetComponent<AI_Interact>();
        aware = GetComponent<AI_Aware>();
        canUpset = true;
    }
	
	// Update is called once per frame
	void Update () {

        if (isUpset)
        {
            //anim_Bounce.Play("Creature_Bounce");
            if (windowRandom == 0){
                Vector3 lookPos = windowPos1.transform.position - transform.position;
                lookPos.y = 0;
                Quaternion rotation = Quaternion.LookRotation(lookPos);
                transform.rotation = Quaternion.Slerp(this.transform.rotation, rotation, Time.deltaTime * 2f);
            }
            else {
                Vector3 lookPos = windowPos2.transform.position - transform.position;
                lookPos.y = 0;
                Quaternion rotation = Quaternion.LookRotation(lookPos);
                transform.rotation = Quaternion.Slerp(this.transform.rotation, rotation, Time.deltaTime * 2f);
            }
            transform.Translate(Vector3.forward * stats.movSpeed * Time.deltaTime);

            var dist1 = Vector3.Distance(transform.position, windowPos1.transform.position);
            if (dist1 <= 1 && stateMachine.State == AI_StateMachine.state.Upset)
            {
                stateMachine.State = AI_StateMachine.state.WindowSlam;
                canUpset = true;
                isUpset = false;
            }
            var dist2 = Vector3.Distance(transform.position, windowPos2.transform.position);
            if (dist2 <= 1 && stateMachine.State == AI_StateMachine.state.Upset)
            {
                stateMachine.State = AI_StateMachine.state.WindowSlam;
                canUpset = true;
                isUpset = false;
            }
        }
	}

    public void Upset()
    {
        if (canUpset)
        {
            windowRandom = Random.Range(0, 2);
            StopAllCoroutines();
            StartCoroutine(UpsetCycle());
            canUpset = false;
        }
    }

    IEnumerator UpsetCycle()
    {
        isUpset = true;
        yield return new WaitForSeconds(0);
    }
}
