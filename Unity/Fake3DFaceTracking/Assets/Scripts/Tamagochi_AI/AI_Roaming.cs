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
    private int movInputZ;
    private int minMax;
    private int xOrz;
    private int repeatWalkCycle;
    private int randomRotDuration;

    public float directionTimer;

    public Vector3 movForward;
    public Vector3 movSideways;

    private bool canChangeDirection;
    public bool canRoam;
    private bool canRotate;
    public bool canMovForward;
    private bool changeRepeatDirection;
    
    // Use this for initialization
	void Start () {
        stats = GetComponent<AI_Variables>();
        controller = GetComponent<AI_Controller>();
        stats.movDirection = Vector3.zero;
        idle = GetComponent<AI_Idle>();
        hitDistance = 3f;
        canRoam = true;
	}
	
	// Update is called once per frame
	void Update () {
        //transform.Translate(movDirection * Time.deltaTime, Space.World);

        DirectionBlocked();
        Debug.DrawRay((this.transform.position+new Vector3(0,.3f,0)) + this.GetComponent<CapsuleCollider>().center, stats.movDirection * hitDistance, Color.yellow);
        //Rotate To New Direction
        if (canRotate)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(stats.movDirection), .05f);

        //Move Forward
        if (canMovForward)
            transform.Translate(Vector3.forward * stats.movSpeed * Time.deltaTime);
    }

    public void Roaming()
    {
        if (canRoam)
        {
            StopAllCoroutines();
            StartCoroutine(WalkCycle());
            canRoam = false;
        }
    }

    IEnumerator WalkCycle()
    {
        canRotate = false;
        canMovForward = false;

        //Repeat WalkCycle - Chance = 50% 
        repeatWalkCycle = Mathf.RoundToInt(Random.Range(0, 2));
        if (repeatWalkCycle == 1)
            canChangeDirection = true;
        if (repeatWalkCycle == 0)
            canChangeDirection = false;

        if (canChangeDirection || hit.collider != null || changeRepeatDirection)
            NewDirection();

        if (repeatWalkCycle == 0)
        {
            canMovForward = false;
            canRotate = true;
            randomRotDuration = Random.Range(0, 2);
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

    //Change movDirection
    public void DirectionBlocked()
    {
        //Check If movDirection Vector Collides
        if (Physics.Raycast((this.transform.position + new Vector3(0, .3f, 0)) + this.GetComponent<CapsuleCollider>().center, stats.movDirection, out hit, hitDistance))
        {
            NewDirection();
            if (hit.collider != null)
                changeRepeatDirection = true;
            else
                canChangeDirection = false;
        }
    }

    private void NewDirection()
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
        stats.movDirection = ((movForward + movSideways).normalized);

        canChangeDirection = false;
    }
}
