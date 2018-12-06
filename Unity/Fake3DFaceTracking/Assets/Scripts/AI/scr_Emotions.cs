using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Emotions : MonoBehaviour {

    //Reference
    public scr_TamagochiAI_v2 ai;

    public float happyness; // happyness = [0,100]
    //public float hunger;    // hunger = [0,100]
    public float amusement;
    public float energy;

    public float happynessToSad = 30f;
    //public float hungerToHungry = 30f;
    public float amusementToBored = 30f;
    public float energyToTired = 30f;
    public float tiredToSleep = 0f;
    public float sleepToWake = Random.Range(30, 50f);

    public bool isHappy;
    //public bool isSatiated;
    public bool isAmused;
    public bool isEnergetic;
    public bool isAwake;

    public bool isPlaying;
    public bool isSleeping;
    public bool isIdle;
    public bool isWandering;
    public bool isResponding;
    public bool isInteracting;
    public bool inRangeOfToy;


    // Update is called once per frame
    void Update() {

        if (isPlaying)
        {
            happyness += 1;
            energy -= .5f;
            amusement += 1;
            
        }

        if (isSleeping)
        {
            energy += 1;
            amusement += .5f;
            happyness = Random.Range(0, 100); //OnWake, this will determine wheither or not the Tamagochi will wake up happy or grumpy
        }

        if (isIdle || isWandering)
        {
            energy -= .5f;
            amusement -= 1;
            happyness -= .5f;
        }

        if (isResponding)
        {
            energy -= .5f;
            amusement -= 1;
            happyness -= .5f;
        }

        if (isInteracting)
        {
            amusement += 1;
            happyness += 1;
            energy -= 1;
        }


        if (!isHappy)
        {
            Stomping(); //Seeks attention by making sound
            Sad(); //facial expression and behavior goes to sad
        }
        else
        {
            Happy();//facial expression and behavior goes to happy
        }

        if (!isAmused)
        {
            Wandering(); //Walks aimlessly in his box
        }
        else
        {
            if (inRangeOfToy)
            {
                Playing();
            }
        }

        if (!isEnergetic)
        {
            Tired(); // is tired, movement speed is reduced and ignores more frequently
        }
        else
        {
            Energized();//Probably Default face
        }
        if (!isAwake)
        {
            Sleeping();//sleeps for a certain amount of time
        }
        else
        {
            Awake();// 
        }


        // State Bools
        if (happyness <= happynessToSad)
        {
            isHappy = false;
        }
        else
        {
            isHappy = true;
        }


        if (amusement <= amusementToBored)
        {
            isAmused = false;
        }
        else
        {
            isAmused = true;
        }

        if (energy <= energyToTired)
        {
            isEnergetic = false;
        }
        else
        {
            isEnergetic = true;
        }

        if (energy <= tiredToSleep)
        {
            isAwake = false;
        }
        else
        {
            if(energy >= sleepToWake)
            {
                isAwake = true;
            }
        }

        //if (hunger <= 30)
        //{
        //    isSatiated = false;
        //}
        //else
        //{
        //    isSatiated= true;
        //}





    }
    private void Playing()
    {

    }

    private void Wandering()
    {

    }

    private void Sleeping()
    {

    }

    private void Tired()
    {

    }

    private void Stomping()
    {

    }

    private void Sad()
    {

    }

    private void Happy()
    {

    }

    private void Awake()
    {
        
    }

    private void Energized()
    {

    }

}
