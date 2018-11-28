using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_playerFunctions : MonoBehaviour
{
    // Initialize the private variables
    private bool thinkingInit;
    private int randomState;

    private bool idleInit;
    private float idleAlarm;

    private bool wanderingInit;
    private float wanderingAlarm;
    private float wanderingDirection;

    // Initialize the public variables
    public scr_playerStats playerStats;

    public Transform interactionManagerTransform;

    public Transform userHeadTransform;
    public Transform creatureHeadTransform;

    // Think of what state to switch to next
    public void thinking()
    {
        // Run this code once
        if (!thinkingInit)
        {
            // Thinking state start code
            randomState = Random.Range(0, 2);

            // Reset the thinking initialization boolean
            thinkingInit = true;
        }

        // Choose a playerState based on the given random ID
        switch (randomState)
        {
            // Player idle state
            case 0:
                thinkingInit = false;
                playerStats.playerState = scr_playerStats.states.Idle;
                break;

            // Player wandering state
            case 1:
                thinkingInit = false;
                playerStats.playerState = scr_playerStats.states.Wandering;
                break;
        }
    }

    // Idle
    public void idle(float idleDurationMin, float idleDurationMax)
    {
        // Run this code once
        if (!idleInit)
        {
            // Idle state start code
            idleAlarm = Random.Range(idleDurationMin, idleDurationMax);

            // Reset the idle initialization boolean
            idleInit = true;
        }

        /* Run this code every frame
        Decrease the idleAlarm every frame */
        idleAlarm--;

        // Set the playerState to the switch state in order to choose a new state
        if (idleAlarm <= 0f)
        {
            idleInit = false;
            playerStats.playerState = scr_playerStats.states.Thinking;
        }
    }

    // Wander in a random direction
    public void wandering(float movementSpeed, float wanderingDurationMin, float wanderingDurationMax)
    {
        // Run this code once
        if (!wanderingInit)
        {
            // Wandering state start code
            wanderingAlarm = Random.Range(wanderingDurationMin, wanderingDurationMax);
            wanderingDirection = Random.Range(0f, 360f);

            // Reset the wandering initialization boolean
            wanderingInit = true;
        }

        /* Run this code every frame
        Rotate the creature towards the randomly set direction */
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, wanderingDirection, transform.eulerAngles.z);

        // Move the creature forwards
        transform.position += (transform.forward * movementSpeed) * Time.deltaTime;

        // Decrease the wanderingAlarm every frame
        wanderingAlarm--;

        // Set the playerState to the switch state in order to choose a new state
        if (wanderingAlarm <= 0f)
        {
            wanderingInit = false;
            playerStats.playerState = scr_playerStats.states.Thinking;
        }
    }

    // 
    public void respond(float movementSpeed)
    {
        // 
        transform.position = Vector3.MoveTowards(transform.position, interactionManagerTransform.position, movementSpeed / 50f);

        //
        var lookPos = interactionManagerTransform.position - transform.position;
        lookPos.y = 0;
        transform.rotation = Quaternion.LookRotation(lookPos);

        //
        if (transform.position == interactionManagerTransform.position)
            playerStats.playerState = scr_playerStats.states.Interact;
    }

    // 
    public void interact()
    {
        // 
        var lookPos = userHeadTransform.position - creatureHeadTransform.position;
        creatureHeadTransform.rotation = Quaternion.LookRotation(lookPos);

        if (Input.GetKey(KeyCode.Space))
            playerStats.playerState = scr_playerStats.states.Thinking;
    }
}
