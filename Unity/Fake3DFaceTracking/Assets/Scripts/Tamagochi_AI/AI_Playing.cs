using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Playing : MonoBehaviour {

    //Reference
    private AI_Controller controller;
    private AI_StateMachine stateMachine;
    public AI_Variables stats;
    public Transform toy;

    const float EPSILON = 0.1f;

    private bool isPlaying;

	// Use this for initialization
	void Start () {

        controller = GetComponent<AI_Controller>();
        stateMachine = GetComponent<AI_StateMachine>();
        stats = GetComponent<AI_Variables>();
        isPlaying = true;

	}
	
	// Update is called once per frame
	void Update () {

        if (stateMachine.State == AI_StateMachine.state.Playing)
        {
            StopAllCoroutines();
            if ((transform.position - toy.position).magnitude > EPSILON)
            {
                //move towards the toy (target)
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(toy.position.x, 0f, toy.position.z), stats.movSpeed * Time.deltaTime);
                Vector3 targetDir = toy.position - transform.position;
                Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, stats.chaseRotSpeed * Time.deltaTime, 0f);
                newDir.y = 0f;
                transform.rotation = Quaternion.LookRotation(newDir);
            }
        }   
	}

    public void Playing()
    {
        isPlaying = true;
    } 


}
