using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Roaming : MonoBehaviour {

    //Reference
    public Transform interactionManager;
    private Vector3 physicsCentre;
    private RaycastHit hit;
    private float hitDistance;

    private AI_Variables stats;
    private AI_Idle idle;
    private AI_Controller controller;

    private int movInputX;
    private int movInputY;
    private int movInputZ;
    private int minMax;
    private int xOrz;
    private int repeatWalkCycle;
    private int randomRotDuration;

    public float directionTimer;

    public Vector3 movForward;
    public Vector3 movSideways;

    public AudioSource audioSourceJump;
    public AudioSource audioSourceLand;

    private bool canChangeDirection;
    public bool canRoam;
    private bool canRotate;
    public bool canMovForward;
    private bool doStep;
    private bool playAudio;

    //Animations
    //public Animation anim_Bounce;
    
    // Use this for initialization
	void Start () {
        stats = GetComponent<AI_Variables>();
        controller = GetComponent<AI_Controller>();
        stats.movDirection = Vector3.zero;
        idle = GetComponent<AI_Idle>();
        hitDistance = 4.5f;
        canRoam = true;
        doStep = true;
	}
	
	// Update is called once per frame
	void Update () {
        //transform.Translate(movDirection * Time.deltaTime, Space.World)
        Debug.DrawRay(this.transform.position + this.GetComponent<CapsuleCollider>().center, stats.movDirection * (hitDistance/4), Color.yellow);
    }

    public void Roaming()
    {
        if (canRoam)
        {
            StopAllCoroutines();
            StartCoroutine(WalkCycle());
            canRoam = false;
        }
        //Rotate
        //New Direction
        if(canRotate)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(stats.movDirection), .05f);

        //Move Forward
        if (canMovForward)
        {
            transform.Translate(Vector3.forward * stats.movSpeed * Time.deltaTime);
            //anim_Bounce.Play("Creature_Bounce");

            if (playAudio)
            {
                StartCoroutine(AudioCycle());
                playAudio = false;
            }
        }
        else
            playAudio = true;
    }

    IEnumerator AudioCycle()
    {
        yield return new WaitForSeconds(.25f);
        audioSourceJump.Play();

        yield return new WaitForSeconds(.35f);
        audioSourceLand.Play();
    }

    IEnumerator WalkCycle()
    {
        canRotate = false;
        canMovForward = false;

        //Repeat WalkCycle - Chance = 50% 
        repeatWalkCycle = Random.Range(0, 2);
        if(repeatWalkCycle == 0)
            canChangeDirection = true;
        if (repeatWalkCycle == 1)
            canChangeDirection = false;

        if (repeatWalkCycle == 0)
        {
            if (canChangeDirection)
            {
                movInputX = Random.Range(-1, 2);
                movInputZ = Random.Range(-1, 2);

                if (movInputX == 0 && movInputZ == 0)
                {
                    xOrz = Random.Range(0, 2);
                    minMax = Random.Range(0, 2);

                    if (xOrz == 0)
                        if (minMax == 0)
                            movInputX = -1;
                    if (minMax == 1)
                        movInputX = 1;
                    if (xOrz == 1)
                        if (minMax == 0)
                            movInputZ = -1;
                    if (minMax == 1)
                        movInputZ = 1;
                }
                movSideways = transform.right * movInputX * Time.deltaTime;
                movForward = transform.forward * movInputZ * Time.deltaTime;

                //Vector3 movDirection
                stats.movDirection = ((movForward + movSideways).normalized * stats.movSpeed);

                canChangeDirection = false;
            }
            canMovForward = false;
            canRotate = true;
            randomRotDuration = Random.Range(0, 2);
            DirectionBlocked();
            yield return new WaitForSeconds(randomRotDuration);
            canRotate = false;
            yield return new WaitForSeconds(0);
            canMovForward = true;

            yield return new WaitForSeconds(stats.walkDuration);
            canMovForward = false;
            controller.canNewState = true;
            yield return new WaitForSeconds(.1f);
            canRoam = true;
        }
        else
        {
            if (canChangeDirection)
            {
                movInputX = Random.Range(-1, 2);
                movInputZ = Random.Range(-1, 2);

                if (movInputX == 0 && movInputZ == 0)
                {
                    xOrz = Random.Range(0, 2);
                    minMax = Random.Range(0, 2);

                    if (xOrz == 0)
                        if (minMax == 0)
                            movInputX = -1;
                    if (minMax == 1)
                        movInputX = 1;
                    if (xOrz == 1)
                        if (minMax == 0)
                            movInputZ = -1;
                    if (minMax == 1)
                        movInputZ = 1;
                }
                movSideways = transform.right * movInputX * Time.deltaTime;
                movForward = transform.forward * movInputZ * Time.deltaTime;

                //Vector3 movDirection
                stats.movDirection = ((movForward + movSideways).normalized * stats.movSpeed);

                canChangeDirection = false;
            }

            DirectionBlocked();
            //Move Forward DUration
            canRotate = true;
            yield return new WaitForSeconds(1);
            canMovForward = true;

            yield return new WaitForSeconds(stats.walkDuration);

            canMovForward = false;
            canRotate = false;
            canRoam = true;
        }      
    }

    //Reverses movDirection
    public void DirectionBlocked()
    {
        //Check If movDirection Vector Collides
        if (Physics.Raycast(this.transform.position + this.GetComponent<CapsuleCollider>().center, stats.movDirection, out hit, hitDistance))
        {
            Vector3 lookPos = interactionManager.transform.position - transform.position;
            lookPos.y = 0;
            //stats.movDirection = lookPos;
            stats.movDirection = -stats.movDirection;
        }
    }

    IEnumerator WalkAnimationCycle()
    {
        yield return new WaitForSeconds(.5f);

        yield return new WaitForSeconds(.5f);
        doStep = true;
    }
}
