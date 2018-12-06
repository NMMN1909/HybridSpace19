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

    // 
    public enum states { Idle, Wandering, Respond, Interact, Annoy, Wake, Sleep, Playing };

    // Initialize the public variables
    public states playerState;
    public float movementSpeed;
    public float wanderingDurationMin;
    public float wanderingDurationMax;
    public float idleDurationMin;
    public float idleDurationMax;

    public float energy;
    public float energeticToTired;
    public float tiredToSleep;
    public float sleepToEnergetic;
    public float amusement;
    public float amusementToBored;
    public float happiness;
    public float happyToSad;
    public float happyToPlay;
    public bool isAwake;

    // Use this for initialization
    void Start()
    {
        isAwake = true;
        energy = 100f;
        amusement = 100f;
        amusementToBored = 50f;
        energeticToTired = 30f;
        tiredToSleep = 0f;
        sleepToEnergetic = 40f;
        happyToPlay = 60f;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerState != scr_playerStats.states.Sleep)
        {
            energy -= .05f;
            amusement -= .2f;
        }

        if (!isAwake && energy <= sleepToEnergetic)
        {
            energy += .2f;
        }

        if (Input.GetKeyDown(KeyCode.Z))
            energy += 10;
        if (Input.GetKeyDown(KeyCode.X))
            amusement += 10f;
        if (Input.GetKeyDown(KeyCode.C))
            happiness += 10f;

        if (amusement < 0)
            amusement = 0;
        if (happiness < 0)
            happiness = 0;


        energySlider.value = energy /100;
        happySlider.value = happiness / 100;
        amusementSlider.value = amusement/ 100;
    }
}
