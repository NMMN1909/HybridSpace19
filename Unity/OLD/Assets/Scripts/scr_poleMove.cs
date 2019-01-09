using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_poleMove : MonoBehaviour
{
    // Initialize the public variables
    public float movementSpeed;
    public float moveAlarmDuration;
    public scr_playerStats playerStats;

    // Initialize the private variables
    float moveAlarm;

	// Use this for initialization
	void Start ()
    {
		
	}
	
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

        if (moveAlarm > 0f && playerStats.playerState != scr_playerStats.states.Sleep && playerStats.playerState != scr_playerStats.states.Wake && playerStats.playerState != scr_playerStats.states.Float)
            playerStats.playerState = scr_playerStats.states.Playing;
        else
        {
            if (playerStats.playerState == scr_playerStats.states.Playing)
                playerStats.playerState = scr_playerStats.states.Idle;
        }

        moveAlarm--;
    }
}
