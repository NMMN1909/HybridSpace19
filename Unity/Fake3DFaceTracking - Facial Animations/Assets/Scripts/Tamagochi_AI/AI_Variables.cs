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

    public float chaseRotSpeed;

    //Conditions
    public float energy;
    public float happiness;
    public float amusement;
    public float attention;

    public float attentionToNotice;
    public float attentionToAware;
    public float attentionToInteract;

    public float energyToWake;
    public float amuseToAngry;
    public float happyToSad;
    public float happinessToNeural;

    public bool isAwake;

    public int idleSuccessRate;
    public int roamingSuccessRate;
    public int noticeSuccessRate;




    //Facial Expression Values
    public float energyToTired;
    public float happinessToHappy;
    public float happinessToSad;
    public float amusementToSad;
    public float amusementToNeutral;
    public float happinessToUpset;
    public float amusementToUpset;

    // Use this for initialization
    void Start () {
        directionDuration = 30f;
        idleDuration = 3f;
        noticeDuration = 1.5f;
        movSpeed = 2f;
        walkDuration = .75f;
        awareDuration = 1.2f;
        windowSlamDuration = 8f; //Default 3f
        isAwake = true;
        amuseToAngry = 20f;
        happyToSad = 20f;

        //Facial Values Init
        energyToTired = 25;
        happinessToHappy = 80;
        happinessToSad = 1;
        amusementToSad = 1;
        happinessToNeural = 25;
        amusementToNeutral = 25;
        happinessToUpset = 50;
        amusementToUpset = 25;

    //Chance of Behavior State (20%(1/5) = fairly frequent)
    idleSuccessRate = 10;
        roamingSuccessRate = 95;
        noticeSuccessRate = 15;

        attentionToInteract = 40f;

        chaseRotSpeed = 6f;

        energy = 100f;
        happiness = 80f;
        amusement = 80f;
        energyToWake = 100;
    }
}
