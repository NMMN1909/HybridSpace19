using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Roaming : MonoBehaviour {

    //Reference
    public Transform interactionManager;
    private Vector3 physicsCentre;
    public RaycastHit hit;
    private float hitDistance;

    private AI_Variables stats;
    private AI_Idle idle;
    private AI_Controller controller;
    private AI_StateMachine stateMachine;

    private float movInputX;
    private float movInputZ;
    private int minMax;
    private int xOrz;
    private int repeatWalkCycle;
    private float randomRotDuration;

    public float directionTimer;

    public Vector3 movForward;
    public Vector3 movSideways;

    public bool canChangeDirection;
    public bool canRoam;
    private bool canRotate;
    public bool canMovForward;
    public bool changeMovCheckDirection;
    
    // Use this for initialization
	void Start () {
        stats = GetComponent<AI_Variables>();
        controller = GetComponent<AI_Controller>();
        stateMachine = GetComponent<AI_StateMachine>();
        idle = GetComponent<AI_Idle>();
        hitDistance = 3f;
        canRoam = true;
        NewDirection();
    }
	
	// Update is called once per frame
	void Update () {
        //transform.Translate(movDirection * Time.deltaTime, Space.World);
        if (controller.stopAllCoroutines)
            StartCoroutine(StopCoroutines());

        if(stateMachine.State != AI_StateMachine.state.Roaming)
        {
            canMovForward = false;
            canRoam = true;
            canRotate = false;
            canRotate = false;
            StopAllCoroutines();
        }
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
            MoveRayCastCheck();
            if (changeMovCheckDirection)
                NewDirection();             
            else
            {
                StartCoroutine(WalkCycle());
                canRoam = false;
            }
        }
    }

    IEnumerator WalkCycle()
    {
        canRotate = false;
        canMovForward = false;

            repeatWalkCycle = Mathf.RoundToInt(Random.Range(0, 8));
            if (repeatWalkCycle > 5)
                canChangeDirection = true;
            if (repeatWalkCycle == 0)
                canChangeDirection = false;

        canRotate = true;
        if (repeatWalkCycle == 0)
        {
            randomRotDuration = Random.Range(1, 2);
            yield return new WaitForSeconds(randomRotDuration);
            canMovForward = true;
            yield return new WaitForSeconds(stats.walkDuration);
            canMovForward = false;
            NewDirection();
            StartCoroutine(controller.NewState());
        }
        else
        {
            randomRotDuration = Random.Range(0f, 1f);
            yield return new WaitForSeconds(randomRotDuration);
            canMovForward = true;
            yield return new WaitForSeconds(stats.walkDuration);
            canMovForward = false;
            canRotate = false;
            yield return new WaitForSeconds(Random.Range(0f, .2f));
            canRoam = true;
            if(canChangeDirection)
                NewDirection();
        }
    }

    //Change movDirection
    public void MoveRayCastCheck()
    {
        //Check If movDirection Vector Collides
        if (Physics.Raycast((this.transform.position + new Vector3(0, .3f, 0)) + this.GetComponent<CapsuleCollider>().center, stats.movDirection, out hit, hitDistance))
            changeMovCheckDirection = true;
        else
            changeMovCheckDirection = false;
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
            {
                if (minMax == 0)
                    movInputX = -1;
                if (minMax == 1)
                    movInputX = 1;
            }

            if (xOrz == 1)
            {
                if (minMax == 0)
                    movInputZ = -1;
                if (minMax == 1)
                    movInputZ = 1;
            }
        }
        movSideways = transform.right * movInputX;
        movForward = transform.forward * movInputZ;

        //Vector3 movDirection
        stats.movDirection = ((movForward + movSideways).normalized);
    }

    private IEnumerator StopCoroutines()
    {
        StartCoroutine(controller.NewState());
        controller.stopAllCoroutines = false;
        yield return new WaitForSeconds(.01f);
        StopAllCoroutines();
    }
}
