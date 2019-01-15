using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_poleMove : MonoBehaviour
{
    // Initialize the public variables
    public float moveAlarmDuration;
    public AI_StateMachine playerStats;
    public AI_Variables stats;
    public Transform[] waypointTarget;
    public int targetID;
    public AI_Sleep sleep;
    public float moveAlarm;
	
	// Update is called once per frame
	void Update ()
    {
        /*
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += (-transform.right * movementSpeed) * Time.deltaTime;
            moveAlarm = moveAlarmDuration;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position += (transform.right * movementSpeed) * Time.deltaTime;
            moveAlarm = moveAlarmDuration;
        }
        */

        var dist = Vector3.Distance(transform.position, waypointTarget[targetID].position);
        transform.position = Vector3.MoveTowards(transform.position, waypointTarget[targetID].position, dist * .1f);

        if (moveAlarm > 0f && playerStats.State != AI_StateMachine.state.Sleep && playerStats.State != AI_StateMachine.state.Wake && playerStats.State != AI_StateMachine.state.Float && stats.energy > 5 && !sleep.disturbedBool && stats.isAwake)
            playerStats.State = AI_StateMachine.state.Playing;
        else
        {
            if (playerStats.State == AI_StateMachine.state.Playing)
                playerStats.State = AI_StateMachine.state.Default;
        }

        moveAlarm--;
    }
}
