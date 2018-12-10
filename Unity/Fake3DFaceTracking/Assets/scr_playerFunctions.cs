using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_playerFunctions : MonoBehaviour
{
    //Reference
    public scr_TamagochiVision vision;
    public scr_playerGrounded groundCheck;
    public Rigidbody rb;

    public GameObject modelAwake;
    public GameObject modelAsleep;

    public Material[] creatureMaterials;
    public Material playerMaterial;
    public Renderer _renderer;

    private float materialLerpProgress;
    private Material targetMat0;
    private Material targetMat1;
    
    // Initialize the private variables
    private bool thinkingInit;
    private int randomState;

    private bool idleInit;
    private float idleAlarm;

    private bool wanderingInit;
    private float wanderingAlarm;
    private float wanderingDirection;

    private bool interactInit;
    private float interactAlarm;

    private bool colorizeInit;

    private bool isGrowing = true;

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

        // Go to sleep when energy is low
        if (playerStats.energy < playerStats.tiredToSleep && playerStats.isAwake)
            playerStats.playerState = scr_playerStats.states.Sleep;

        // 
        if (playerStats.amusement < playerStats.amusementToBored && playerStats.isAwake)
            playerStats.playerState = scr_playerStats.states.Playing;

        //
        if (playerStats.amusement >= 100f)
            playerStats.playerState = scr_playerStats.states.Wandering;

        /*
        if (playerStats.energy < playerStats.tiredToSleep && playerStats.isAwake)
        {
            playerStats.playerState = scr_playerStats.states.Sleep;
        }
        else if (playerStats.happiness > playerStats.happyToPlay && playerStats.amusement > playerStats.amusementToBored && playerStats.isAwake)
        {
            playerStats.playerState = scr_playerStats.states.Playing;
        }
        else if (playerStats.amusement < playerStats.amusementToBored && playerStats.isAwake)
        {
            playerStats.playerState = scr_playerStats.states.Wandering;
        }
        */

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

        playerStats.playerState = scr_playerStats.states.Wandering;
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
        // Run this code once
        if (!interactInit)
        {
            // Idle state start code
            interactAlarm = 1750f;

            // Reset the idle initialization boolean
            interactInit = true;
        }

        interactAlarm--;

        if (interactAlarm <= 0f)
        {
            interactInit = false;
            creatureHeadTransform.rotation = Quaternion.Euler(0f, 0f, 0f);
            playerStats.playerState = scr_playerStats.states.Idle;
        }

        var lookPos = userHeadTransform.position - creatureHeadTransform.position;
        //creatureHeadTransform.rotation = Quaternion.LookRotation(lookPos);

        creatureHeadTransform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(lookPos), .1f);

        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, creatureHeadTransform.eulerAngles.y, transform.eulerAngles.z);
    }

    // 
    public void sleep()
    {
        // 
        playerStats.isAwake = false;
        modelAwake.SetActive(false);
        modelAsleep.SetActive(true);
    }

    //
    public void wake()
    {
        playerStats.isAwake = true;
        playerStats.amusement = Random.Range(0, 100);
        playerStats.playerState = scr_playerStats.states.Idle;

        modelAwake.SetActive(true);
        modelAsleep.SetActive(false);
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
            playerStats.amusement += .075f;
        }
    }

    public void grow(float speed, float target)
    {
        if (transform.localScale.x >= target && isGrowing)
        {
            isGrowing = false;
            playerStats.playerState = scr_playerStats.states.Wandering;
        }

        if (transform.localScale.x <= .1478281f && !isGrowing)
        {
            isGrowing = true;
            playerStats.playerState = scr_playerStats.states.Wandering;
        }

        if (isGrowing)
            transform.localScale += new Vector3(speed, speed, speed);
        else
            transform.localScale -= new Vector3(speed, speed, speed);
    }

    public void colorize()
    {
        if (!colorizeInit)
        {
            materialLerpProgress = 0f;

            targetMat0 = playerMaterial;
            targetMat1 = creatureMaterials[Mathf.RoundToInt(Random.Range(0f, creatureMaterials.Length - 1f))];

            while (targetMat1.color.Equals(targetMat0.color))
                targetMat1 = creatureMaterials[Mathf.RoundToInt(Random.Range(0f, creatureMaterials.Length - 1f))];

            colorizeInit = true;
        }

        materialLerpProgress += .01f;
        playerMaterial.Lerp(targetMat0, targetMat1, materialLerpProgress);

        if (materialLerpProgress >= 1f)
        {
            colorizeInit = false;
            playerStats.playerState = scr_playerStats.states.Wandering;
        }
    }
}
