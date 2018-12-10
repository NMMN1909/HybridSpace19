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
    public enum states { Idle, Wandering, Respond, Interact, Annoy, Wake, Sleep, Playing, Grow, Colorize };

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
    public float amusementToHappy;
    public float amusementToSad;

    public bool isAwake;

    // Use this for initialization
    void Start()
    {
        isAwake = true;
        energy = 100f;
        amusement = 50f;
        amusementToBored = 25f;
        energeticToTired = 30f;
        tiredToSleep = 0f;
        sleepToEnergetic = 100f;
        happyToPlay = 60f;
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

        if (Input.GetKeyDown(KeyCode.Z))
            energy += 10f;

        if (Input.GetKeyDown(KeyCode.X))
            amusement += 10f;

        if (Input.GetKeyDown(KeyCode.C))
            happiness += 10f;

        if (amusement < 0f)
            amusement = 0f;
        if (happiness < 0f)
            happiness = 0f;


        energySlider.value = energy / 100f;
        happySlider.value = happiness / 100f;
        amusementSlider.value = amusement / 100f;
    }
}
