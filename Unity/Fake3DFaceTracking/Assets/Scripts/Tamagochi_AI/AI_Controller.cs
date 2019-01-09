using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AI_Controller : MonoBehaviour {

    //Reference
    private AI_Variables stats;
    private AI_StateMachine stateMachine;
    private AI_Roaming roaming;
    private AI_Aware aware;
    private AI_Sleep sleep;

    private int idleFailRate;
    public bool canNewState;

    private float awareTimer;
    private float awareCounter;
    private int awareRandom;
    private int respondCounter;

    //Debug Reference
    public Slider energySlider;
    public Slider happySlider;
    public Slider amusementSlider;
    public Slider affectionSlider;

    // Use this for initialization
    void Start () {
        stats = GetComponent<AI_Variables>();
        aware = GetComponent<AI_Aware>();
        roaming = GetComponent<AI_Roaming>();
        stateMachine = GetComponent<AI_StateMachine>();
        sleep = GetComponent<AI_Sleep>();
        stateMachine.State = AI_StateMachine.state.Default;
        awareTimer = 200;
        respondCounter = 0;

        canNewState = true;
    }
	
	// Update is called once per frame
	void Update () {
        energySlider.value = stats.energy / 100f;
        happySlider.value = stats.happiness / 100f;
        amusementSlider.value = stats.amusement / 100f;
        affectionSlider.value = stats.attention / 100f;
    }

    public void Brain()
    {
        //Behavior
        if (stats.energy >= 70f)
        {
            stats.idleSuccessRate = 10;
        }
        else if (stats.energy >= 40f && stats.energy < 70f)
        {
            stats.idleSuccessRate = 20;
        }

        if (stats.energy <= 0 && stats.isAwake)
        {
            stateMachine.State = AI_StateMachine.state.Sleep;
            stats.isAwake = false;
        }

        if (stats.attention > 70 && stateMachine.State != AI_StateMachine.state.Interact)
            stateMachine.State = AI_StateMachine.state.Respond;

        if (stateMachine.State == AI_StateMachine.state.Roaming || stateMachine.State == AI_StateMachine.state.Default)
        {
            awareCounter += 1;
            if (awareCounter > awareTimer)
            {
                awareRandom = Random.Range(0, 2);
                if (awareRandom == 1)
                {
                    aware.Aware();
                    respondCounter += 1;
                }
                awareCounter = 0;
            }
        }
        else
            awareCounter = 0;

        if(respondCounter == 6)
        {
            stateMachine.State = AI_StateMachine.state.Respond;
            stats.attention += 100;
            respondCounter = 0;
        }



        //New State
        if (canNewState)
        {
            idleFailRate = Random.Range(0, 101);
            if((stats.noticeSuccessRate > idleFailRate) && (stats.attentionToNotice >= stats.attention && stateMachine.State != AI_StateMachine.state.Respond && stateMachine.State != AI_StateMachine.state.Sleep && stateMachine.State != AI_StateMachine.state.Upset))
                stateMachine.State = AI_StateMachine.state.Notice;
            
            else if (stats.idleSuccessRate > idleFailRate && stateMachine.State != AI_StateMachine.state.Respond && stateMachine.State != AI_StateMachine.state.Sleep && stateMachine.State != AI_StateMachine.state.Notice && stateMachine.State != AI_StateMachine.state.Upset)
                stateMachine.State = AI_StateMachine.state.Default;
            else if (stats.roamingSuccessRate > idleFailRate && stateMachine.State != AI_StateMachine.state.Respond && stateMachine.State != AI_StateMachine.state.Sleep && stateMachine.State != AI_StateMachine.state.Notice && stateMachine.State != AI_StateMachine.state.Upset)
                stateMachine.State = AI_StateMachine.state.Roaming;

            canNewState = false;
        }

        DebugController();
    }

    public void Senses()
    {
        //Detect GameWorld
        //roaming.DirectionBlocked();
    }

    public void Cells()
    {
        if (stats.isAwake)
        {
            stats.energy -= .02f;
            stats.attention -= .1f;
            stats.amusement -= .1f;
            if (stateMachine.State != AI_StateMachine.state.Playing)
                stats.happiness -= .1f;
        }

        //Min Stats Value
        if (stats.energy <= 0)
            stats.energy = 0;
        if (stats.attention <= 0)
            stats.attention = 0;
        if (stats.happiness <= 0)
            stats.happiness = 0;
        if (stats.amusement <= 0)
            stats.amusement = 0;

        //Max Stats Value
        if (stats.energy > 100)
            stats.energy = 100;
        if (stats.attention > 100)
            stats.attention = 100;
        if (stats.happiness > 100)
            stats.happiness = 100;
        if (stats.amusement > 100)
            stats.amusement = 100;
    }

    private void DebugController()
    {
        if (Input.GetKeyDown(KeyCode.Z))
            stats.energy += 10f;

        if (Input.GetKeyDown(KeyCode.X))
            stats.happiness += 10f;

        if (Input.GetKeyDown(KeyCode.C))
            stats.amusement += 10f;

        if (Input.GetKeyDown(KeyCode.V))
            stats.attention += 10f;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            stats.attention += Random.Range(20, 35);
            aware.Aware();
        }

        if (Input.GetKeyDown(KeyCode.A))
            stateMachine.State = AI_StateMachine.state.Default;

        if (Input.GetKeyDown(KeyCode.S))
            stateMachine.State = AI_StateMachine.state.Roaming;

        if (Input.GetKeyDown(KeyCode.D))
            aware.Aware();

        if (Input.GetKeyDown(KeyCode.E))
            stateMachine.State = AI_StateMachine.state.Respond;

        if (Input.GetKeyDown(KeyCode.R))
            stateMachine.State = AI_StateMachine.state.Upset;
    }
}
