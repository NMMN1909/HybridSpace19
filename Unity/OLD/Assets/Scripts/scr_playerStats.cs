using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scr_playerStats : MonoBehaviour
{
    //References
    public Slider energySlider;
    public Slider happySlider;
    public Slider amusementSlider;
    public Slider affectionSlider;

    // 
    public enum states { Idle, Wandering, Respond, Interact, Annoy, Wake, Sleep, Playing, Grow, Colorize, Float };

    // Initialize the public variables
    public states playerState;
    public int randomState;

    public float movementSpeed;
    public float wanderingDurationMin;
    public float wanderingDurationMax;
    public float idleDurationMin;
    public float idleDurationMax;
    public float chaseSpeed = 4f;
    public float chaseRotSpeed = 10f;
    public float jumpForce = 4f;

    public float energy;
    public float affection;
    public float energeticToTired;
    public float tiredToSleep;
    public float sleepToEnergetic;
    public float amusement;
    public float amusementToBored;
    public float happiness;
    public float happyToSad;
    public float happyToPlay;
    public float amusementToHappy;
    public float amusementToSad;

    public float wanderingPauseAlarm;
    public float wanderingPauseCounter = 0f;
    public float idleAlarm;
    public float wanderingAlarm;
    public float wanderingDirection;
    public float wanderingStopAlarm;
    public float interactAlarm;

    public bool isAwake;
    public bool isGrowing = true;
    public bool thinkingInit;
    public bool idleInit;
    public bool interactInit;
    public bool wanderingInit;
    public bool wanderingPauseBool = true;
    public bool colorizeInit;
    public bool wanderingStopBool;

    // Use this for initialization
    void Start()
    {
        isAwake = true;
        energy = 100f;
        amusement = 50f;
        affection = 0f; 
        amusementToBored = 25f;
        energeticToTired = 30f;
        tiredToSleep = 0f;
        sleepToEnergetic = 100f;
        amusementToHappy = 75f;
        amusementToSad = 35f;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerState != scr_playerStats.states.Sleep)
        {
            energy -= .025f;
            amusement -= .025f;
        }

        if (!isAwake && energy <= sleepToEnergetic)
            energy += .2f;

        if (amusement >= amusementToHappy)
            happiness += .2f;

        if (amusement <= amusementToSad)
            happiness -= .05f;

        if (amusement < 0f)
            amusement = 0f;
        if (happiness < 0f)
            happiness = 0f;
        if (affection < 0f)
            affection = 0f;


        energySlider.value = energy / 100f;
        happySlider.value = happiness / 100f;
        amusementSlider.value = amusement / 100f;
        affectionSlider.value = affection / 100f;
    }
}
