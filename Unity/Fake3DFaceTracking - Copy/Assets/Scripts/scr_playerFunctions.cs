using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_playerFunctions : MonoBehaviour
{
    //Reference
    public Rigidbody rb;
    public Transform target;
    public scr_playerStats playerStats;
    public Transform interactionManagerTransform;
    public Transform userHeadTransform;
    public Transform creatureHeadTransform;
    public Animation animWalk;

    public GameObject modelAwake;
    public GameObject modelAsleep;
    public GameObject modelFloat;

    public Material[] creatureMaterials;
    public Material playerMaterial;
    public Renderer _renderer;

    private float materialLerpProgress;
    private Material targetMat0;
    private Material targetMat1;

    const float EPSILON = 0.1f;

    // Think of what state to switch to next
    public void thinking()
    {
        // 


        // Go to sleep when energy is low
        if (playerStats.energy < playerStats.tiredToSleep && playerStats.isAwake)
            playerStats.playerState = scr_playerStats.states.Sleep;

        /*
        if (playerStats.amusement >= 100f)
            playerStats.playerState = scr_playerStats.states.Wandering;
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
        if (!playerStats.idleInit)
        {
            // Idle state start code
            playerStats.idleAlarm = Random.Range(idleDurationMin, idleDurationMax);

            // Reset the idle initialization boolean
            playerStats.idleInit = true;
        }
        playerStats.playerState = scr_playerStats.states.Wandering;
    }

    // Wander in a random direction
    public void wandering(float movementSpeed, float wanderingDurationMin, float wanderingDurationMax)
    {
        animWalk.Play("Creature_Walk");
        if (playerStats.wanderingPauseBool)
        {
            playerStats.wanderingPauseAlarm = Random.Range(5, 30);
            playerStats.wanderingPauseBool = false;
            playerStats.wanderingInit = false;
        }

        if (playerStats.wanderingPauseCounter > playerStats.wanderingPauseAlarm)
        {
            // Run this code once
            if (!playerStats.wanderingInit)
            {
                // Wandering state start code
                playerStats.wanderingAlarm = Random.Range(wanderingDurationMin, wanderingDurationMax);
                playerStats.wanderingDirection = Random.Range(0f, 360f);

                // Reset the wandering initialization boolean
                playerStats.wanderingInit = true;
                playerStats.wanderingPauseBool = true;
                playerStats.wanderingPauseCounter = 0f;
            }
        }
        playerStats.wanderingPauseCounter += .1f;

        /* Run this code every frame
        Rotate the creature towards the randomly set direction */
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, playerStats.wanderingDirection, transform.eulerAngles.z);

        // Move the creature forwards
        transform.position += (transform.forward * movementSpeed) * Time.deltaTime;
    }

    // 
    public void respond(float movementSpeed)
    {
        animWalk.Play("Creature_Walk");
        // 
        transform.position = Vector3.MoveTowards(transform.position, interactionManagerTransform.position, movementSpeed / 50f);

        //
        var lookPos = interactionManagerTransform.position - transform.position;
        lookPos.y = 0;
        transform.rotation = Quaternion.LookRotation(lookPos);

        //
        var dist = Vector3.Distance(transform.position, interactionManagerTransform.transform.position);
        if (dist <= .1f)
            playerStats.playerState = scr_playerStats.states.Interact;
    }

    // 
    public void interact()
    {
        // Run this code once
        if (!playerStats.interactInit)
        {
            // Idle state start code
            playerStats.interactAlarm = 1750f;

            // Reset the idle initialization boolean
            playerStats.interactInit = true;
        }

        playerStats.interactAlarm--;

        if (playerStats.interactAlarm <= 0f)
        {
            playerStats.interactInit = false;
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
    
    public void playing()
    {
        if ((transform.position - target.position).magnitude > EPSILON)
        {
            //move towards the toy (target)
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.position.x, 0f, target.position.z), .025f);
            Vector3 targetDir = target.position - transform.position;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, playerStats.chaseRotSpeed, 0f);
            newDir.y = 0f;
            transform.rotation = Quaternion.LookRotation(newDir);
            playerStats.amusement += .075f;
        }

        animWalk.Play("Creature_Walk");
    }

    public void grow(float speed, float target)
    {
        if (transform.localScale.x >= target && playerStats.isGrowing)
        {
            playerStats.affection += 10f;
            playerStats.isGrowing = false;
            playerStats.playerState = scr_playerStats.states.Wandering;
        }

        if (transform.localScale.x <= .1478281f && !playerStats.isGrowing)
        {
            playerStats.affection += 10f;
            playerStats.isGrowing = true;
            playerStats.playerState = scr_playerStats.states.Wandering;
        }

        if (playerStats.isGrowing)
            transform.localScale += new Vector3(speed, speed, speed);
        else
            transform.localScale -= new Vector3(speed, speed, speed);
    }

    public void colorize()
    {
        if (!playerStats.colorizeInit)
        {
            materialLerpProgress = 0f;

            targetMat0 = playerMaterial;
            targetMat1 = creatureMaterials[Mathf.RoundToInt(Random.Range(0f, creatureMaterials.Length - 1f))];

            while (targetMat1.color.Equals(targetMat0.color))
                targetMat1 = creatureMaterials[Mathf.RoundToInt(Random.Range(0f, creatureMaterials.Length - 1f))];

            playerStats.affection += 10f;
            playerStats.colorizeInit = true;
        }

        materialLerpProgress += .01f;
        playerMaterial.Lerp(targetMat0, targetMat1, materialLerpProgress);

        if (materialLerpProgress >= 1f)
        {
            playerStats.colorizeInit = false;
            playerStats.playerState = scr_playerStats.states.Wandering;
        }
    }

    public void fly()
    {
        modelFloat.SetActive(true);
        modelAwake.SetActive(false);

        animWalk.Play("Creature_Float");
        StartCoroutine(FlyAnimation());
    }

    IEnumerator FlyAnimation()
    {
        yield return new WaitForSeconds(8.55f);
        modelFloat.SetActive(false);
        modelAwake.SetActive(true);
        playerStats.playerState = scr_playerStats.states.Idle;
    }
}
