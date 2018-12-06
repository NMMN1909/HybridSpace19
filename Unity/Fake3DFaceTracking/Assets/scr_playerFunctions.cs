using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_playerFunctions : MonoBehaviour
{
    //Reference
    public scr_TamagochiVision vision;
    public scr_playerGrounded groundCheck;
    public Rigidbody rb;
    
    // Initialize the private variables
    private bool thinkingInit;
    private int randomState;

    private bool idleInit;
    private float idleAlarm;

    private bool wanderingInit;
    private float wanderingAlarm;
    private float wanderingDirection;

    public float jumpForce = 4f;


    //Playing Stats
    public Transform target;
    public float chaseSpeed = 4f;
    public float chaseRotSpeed = 10f;
    const float EPSILON = 0.1f;

    [SerializeField]
    private float wanderingPauseAlarm;
    [SerializeField]
    private float wanderingPauseCounter = 0f;
    [SerializeField]
    private bool wanderingPauseBool = true;

    // Initialize the public variables
    public scr_playerStats playerStats;

    public Transform interactionManagerTransform;

    public Transform userHeadTransform;
    public Transform creatureHeadTransform;

    // Think of what state to switch to next
    public void thinking()
    {
        if (playerStats.energy < playerStats.energeticToTired && playerStats.energeticToTired > playerStats.tiredToSleep && playerStats.isAwake)
        {

        }

        if (playerStats.energy < playerStats.tiredToSleep && playerStats.isAwake)
        {
            playerStats.playerState = scr_playerStats.states.Sleep;
        }
        else if (playerStats.happiness > playerStats.happyToPlay && playerStats.isAwake)
        {
            playerStats.playerState = scr_playerStats.states.Playing;
        }
        else if (playerStats.amusement < playerStats.amusementToBored && playerStats.isAwake)
        {
            playerStats.playerState = scr_playerStats.states.Wandering;
        }

        //Tamagochi is Asleep
        if (playerStats.energy > playerStats.sleepToEnergetic && !playerStats.isAwake)
        {
            playerStats.playerState = scr_playerStats.states.Wake;
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
    }

    // Wander in a random direction
    public void wandering(float movementSpeed, float wanderingDurationMin, float wanderingDurationMax)
    {
        if (wanderingPauseBool)
        {
            wanderingPauseAlarm = Random.Range(5, 30);
            wanderingPauseBool = false;
            wanderingInit = false;
        }

        if (wanderingPauseCounter > wanderingPauseAlarm)
        {
            // Run this code once
            if (!wanderingInit)
            {
                // Wandering state start code
                wanderingAlarm = Random.Range(wanderingDurationMin, wanderingDurationMax);
                wanderingDirection = Random.Range(0f, 360f);

                // Reset the wandering initialization boolean
                wanderingInit = true;
                wanderingPauseBool = true;
                wanderingPauseCounter = 0f;
            }
        }
        wanderingPauseCounter += .1f;


        /* Run this code every frame
        Rotate the creature towards the randomly set direction */
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, wanderingDirection, transform.eulerAngles.z);

        // Move the creature forwards
        transform.position += (transform.forward * movementSpeed) * Time.deltaTime;

        if (vision.hitWall)
        {
            wanderingDirection = Random.Range(90f, 270f);
        }
        else
        {
            vision.hitWall = false;
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
        var dist = Vector3.Distance(transform.position, interactionManagerTransform.transform.position);
        if (dist <= 1f)
            playerStats.playerState = scr_playerStats.states.Interact;
    }

    // 
    public void interact()
    {
            var lookPos = userHeadTransform.position - creatureHeadTransform.position;
            creatureHeadTransform.rotation = Quaternion.LookRotation(lookPos);
    
        if (Input.GetKey(KeyCode.Space))
        {
            creatureHeadTransform.rotation = Quaternion.Euler(0f, 0f, 0f);
            playerStats.playerState = scr_playerStats.states.Idle;
        }
    }

    // 
    public void sleep()
    {
        // 
        playerStats.isAwake = false;
    }

    //
    public void wake()
    {
        playerStats.isAwake = true;
        playerStats.energy = Random.Range(40, 100);
        playerStats.amusement = Random.Range(0, 100);
        playerStats.playerState = scr_playerStats.states.Idle;
    }

    public void annoy()
    {
        Debug.Log("WAIT");
        if (groundCheck.grounded)
        {
            this.rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            Debug.Log("hello");
        }
    }
    
    public void playing()
    {
        if ((transform.position - target.position).magnitude > EPSILON)
        {
            //move towards the player
            //transform.position += transform.forward * chaseSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.position.x, 0f, target.position.z), .025f);
            Vector3 targetDir = target.position - transform.position;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, chaseRotSpeed, 0f);
            newDir.y = 0f;
            transform.rotation = Quaternion.LookRotation(newDir);
            playerStats.happiness += .3f;
        }
    }

}
