using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Variables : MonoBehaviour {

    public float movSpeed;
    public float directionDuration;
    public float idleDuration;
    public float walkDuration;
    public Vector3 movDirection;

    public float roamingWaitDuration;
    public float noticeDuration;
    public float awareDuration;
    public float windowSlamDuration;

    //Conditions
    public float energy;
    public float happiness;
    public float amusement;
    public float attention;

    public float attentionToNotice;
    public float attentionToAware;
    public float attentionToInteract;

    public float energyToWake;

    public bool isAwake;

    public int idleSuccessRate;
    public int roamingSuccessRate;
    public int noticeSuccessRate;

    // Use this for initialization
    void Start () {
        directionDuration = 30f;
        idleDuration = 5f;
        noticeDuration = 1.6f;
        movSpeed = 2f;
        walkDuration = .75f;
        awareDuration = 1.2f;
        windowSlamDuration = 10f; //Default 3f
        isAwake = true;

        //Chance of Behavior State (20%(1/5) = fairly frequent)
        idleSuccessRate = 10;
        roamingSuccessRate = 95;
        noticeSuccessRate = 15;

        attentionToInteract = 40f;

        energy = 100f;
        happiness = 30f;
        amusement = 80f;
        energyToWake = 100;
    }
}
