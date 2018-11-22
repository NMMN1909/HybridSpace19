using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_TamagochiAI : MonoBehaviour {

    //Reference
    private Renderer rend;

    //Playing Stats
    public Transform target;
    private float chasespeed;
    const float EPSILON = 0.1f;

    //Color
    private Color32 greenColor;
    private Color32 blueColor;
    private Color32 redColor;
    private Color32 yellowColor;

    //Emotions
    public bool isHappy;
    public bool isSad;
    public bool isNeutral;

    //Timers
    private float sleepingTimer;
    private float actionTimer;

    //ActionDurations
    private float sleepingDuration;
    private float actionDuration;
    private float roamingDuration;
    private float playDuration;
    private float roamDuration;
    private float sleepDuration;

    //Actions
    public bool isAwake;
    public bool isPlaying; //following a ball
    public bool isRoaming; //walking around
    public bool isSleeping; //sleeping
    public bool isGoingToScreen; //walks towards screen
    public bool isDoingTrick; //does a trick
    public bool isJumping; //jumps or stomps 

    //Stats
    public float hunger;
    public float sleepiness;
    public float boredem;
    private float turnAroundSpeed;

    //Collision
    private bool characterCollide;

	// Use this for initialization
	void Start () {
        rend = GetComponentInChildren<Renderer>();

        blueColor = new Color32(36, 126, 217, 255);
        redColor = new Color32(217, 36, 36, 255);
        greenColor = new Color32(54, 130, 51, 255);
        yellowColor = new Color32(255, 196, 100, 255);
        chasespeed = 3;

        turnAroundSpeed = 40;
        actionTimer = 0;
        actionDuration = 100;
        roamingDuration = 550;
        playDuration = 400;
        sleepDuration = 200;
        isAwake = true;
        isPlaying = false;
	}
	
	// Update is called once per frame
	void Update () {

        actionTimer += 1;

        //Action Conditions
        if (isAwake)
        {
            if (isPlaying)
            {
                //doNothing
            }

            else 
            {
                isRoaming = true;
                if (actionTimer >= roamingDuration)
                {
                    isPlaying = true;
                    isRoaming = false;
                    actionTimer = 0;
                    isSleeping = false;
                    isAwake = false;
                }
            }
        }

        else if (isPlaying)
        {
            if (actionTimer >= playDuration)
            {
                isPlaying = false;
                isSleeping = false;
                actionTimer = 0;
                isRoaming = true;
                isAwake = true;
            }
        }

        else if (isSleeping)
        {
            if (actionTimer >= sleepDuration)
            {
                isPlaying = false;
                isSleeping = false;
                actionTimer = 0;
                isRoaming = true;
                isAwake = true;
            }
        }

        //Action Controller
        if (isSleeping)
            Sleeping();
        else if (isPlaying)
            Playing();
        else if (isRoaming)
            Roaming();
	}


    private void Roaming()
    {
        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 8;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 2, layerMask))
        {
            if(hit.collider.gameObject.tag != "Tamagochi")
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                Debug.Log("Did Hit");
                characterCollide = true;              
            }
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1, Color.white);
            Debug.Log("Did not Hit");
            characterCollide = false;
        }

        RaycastHit hitLeftSide;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out hitLeftSide, 2f, layerMask))
        {
            if (hitLeftSide.collider.gameObject.tag != "Tamagochi" && !characterCollide)
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.left) * hitLeftSide.distance, Color.yellow);
                Debug.Log("Did Hit");
                transform.Translate(Vector3.right * Time.deltaTime);
            }
        }

        RaycastHit hitRightSide;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out hitRightSide, 2f, layerMask))
        {
            if (hitRightSide.collider.gameObject.tag != "Tamagochi" && !characterCollide)
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * hitRightSide.distance, Color.yellow);
                Debug.Log("Did Hit");
                transform.Translate(Vector3.left * Time.deltaTime);
            }
        }

        if (!characterCollide)
        {
            transform.Translate(Vector3.forward * Time.deltaTime);
        }
        else
        {
            transform.rotation = Random.rotation;
            //transform.Rotate(Vector3.right * turnAroundSpeed * Time.deltaTime);
            //transform.Translate(Vector3.forward * Time.deltaTime);
            //transform.Rotate(0, turnAroundSpeed * Time.deltaTime, 0, Space.World);
        }
        rend.material.color = greenColor;

        Debug.Log("Roaming = " + isRoaming);
    }

    private void Playing()
    {
        transform.LookAt(target.position);
        if ((transform.position - target.position).magnitude > EPSILON)
            transform.Translate(0.0f, 0.0f, chasespeed * Time.deltaTime);

        rend.material.color = yellowColor;
        Debug.Log("Playing = " + isPlaying);
    }

    private void Sleeping()
    {
        Debug.Log("Sleeping = " + isSleeping);
        //Set the main Color of the Material to green
        rend.material.color = blueColor;
    }
}
