using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Grow : MonoBehaviour {

    private AI_Variables stats;
    private AI_StateMachine stateMachine;

    // Use this for initialization
    void Start () {
        stats = GetComponent<AI_Variables>();
        stateMachine = GetComponent<AI_StateMachine>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Grow(float speed, float target)
    {
        if (transform.localScale.x >= target && stats.isGrowing)
        {
            stats.attention += 10f;
            stats.isGrowing = false;
            stateMachine.State = AI_StateMachine.state.Roaming;
        }

        if (transform.localScale.x <= .1478281f && !stats.isGrowing)
        {
            stats.attention += 10f;
            stats.isGrowing = true;
            stateMachine.State = AI_StateMachine.state.Roaming;
        }

        if (stats.isGrowing)
            transform.localScale += new Vector3(speed, speed, speed);
        else
            transform.localScale -= new Vector3(speed, speed, speed);
    }
}
