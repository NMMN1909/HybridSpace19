using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_poleMove : MonoBehaviour
{
    // Initialize the public variables
    public float movementSpeed;
    public float moveAlarmDuration;
    public AI_StateMachine playerStats;
    public AI_Variables stats;

    // Initialize the private variables
    float moveAlarm;
	
	// Update is called once per frame
	void Update ()
    {
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

        if (moveAlarm > 0f && playerStats.State != AI_StateMachine.state.Sleep && playerStats.State != AI_StateMachine.state.Wake && playerStats.State != AI_StateMachine.state.Float && stats.energy > stats.energyToTired)
            playerStats.State = AI_StateMachine.state.Playing;
        else
        {
            if (playerStats.State == AI_StateMachine.state.Playing)
                playerStats.State = AI_StateMachine.state.Default;
        }

        moveAlarm--;
    }
}
