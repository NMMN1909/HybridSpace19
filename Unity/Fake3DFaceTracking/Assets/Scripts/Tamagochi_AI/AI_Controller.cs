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
    private AI_EmotionState emotionStateMachine;
    public scr_Card card;
    public Script_Arduino arduino;
    public AI_Wake wake;

    private int idleFailRate;
    public bool canNewState;

    private float awareTimer;
    private float awareCounter;
    private int awareRandom;
    private int respondCounter;

    private int upsetTimer;
    private int upsetCounter;
    private bool canUpset;

    public bool stopAllCoroutines;

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
        emotionStateMachine = GetComponent<AI_EmotionState>();
        wake = GetComponent<AI_Wake>();
        awareTimer = 200;
        respondCounter = 0;

        canNewState = true;
        canUpset = true;
        upsetCounter = 0;
        upsetTimer = 3500;
        stopAllCoroutines = false;
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
        if (stats.isAwake)
            upsetCounter += 1;
        else
            upsetCounter -= 1;

        if (stats.energy >= 70f)
            stats.idleSuccessRate = 10;
        else if (stats.energy >= 40f && stats.energy < 70f)
            stats.idleSuccessRate = 20;

        if (stats.energy <= 0)
            roaming.canRoam = false;

        if (stats.energy <= 0 && stats.isAwake && arduino.pointLights.activeSelf == false)
        {
            stateMachine.State = AI_StateMachine.state.Sleep;
            stats.isAwake = false;
            canNewState = false;
        }

        //private int upsetTimer;
        //private int upsetCounter;
        //private bool canUpset;
        if (upsetCounter > upsetTimer)
        {
            canUpset = true;
            upsetCounter = 0;
        }

        if ((stats.amusement < stats.amuseToAngry || stats.happiness < stats.happyToSad) && stateMachine.State != AI_StateMachine.state.WindowSlam && canUpset && stateMachine.State != AI_StateMachine.state.Sleep && stateMachine.State != AI_StateMachine.state.Wake)
        {
            stateMachine.State = AI_StateMachine.state.Upset;
            canUpset = false;
            canNewState = false;
        }

        if (stats.attention > 80 && stateMachine.State != AI_StateMachine.state.Interact && stats.isAwake)
        {
            stateMachine.State = AI_StateMachine.state.Respond;
            canNewState = false;
        }

        if ((stateMachine.State == AI_StateMachine.state.Roaming || stateMachine.State == AI_StateMachine.state.Default) && stateMachine.State != AI_StateMachine.state.Sleep)
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

        if (respondCounter == 6 && stats.isAwake && stateMachine.State != AI_StateMachine.state.Sleep)
        {
            stateMachine.State = AI_StateMachine.state.Respond;
            stats.attention += 100;
            respondCounter = 0;
            canNewState = false;
        }

        //New State
        if (canNewState)
        {
            idleFailRate = Random.Range(0, 101);
            if ((stats.noticeSuccessRate > idleFailRate) && (stats.attentionToNotice >= stats.attention && stateMachine.State != AI_StateMachine.state.Respond && stateMachine.State != AI_StateMachine.state.Sleep && stateMachine.State != AI_StateMachine.state.Upset))
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
            if(stateMachine.State == AI_StateMachine.state.Interact || stateMachine.State == AI_StateMachine.state.Respond)
            {
                stats.amusement -= .025f;
                stats.happiness -= .05f;
                stats.attention -= .1f;
                stats.energy -= .02f;
            }
            else if (stateMachine.State == AI_StateMachine.state.Playing)
            {
                stats.amusement += .15f;
                stats.happiness += .2f;
                stats.attention += .05f;
                stats.energy -= .03f;
            }
            else
            {
                stats.amusement -= .025f;
                stats.happiness -= .05f;
                stats.attention -= .05f; 
                stats.energy -= .02f;
            }
        }
        else
        {
            if (sleep.disturbedBool)
            {
                stats.attention -= .1f;
            }
            else
            {
                stats.energy += .1f;
                stats.attention -= .1f;
            }
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
            stats.energy = 99;
        if (stats.attention > 100)
            stats.attention = 99;
        if (stats.happiness > 100)
            stats.happiness = 99;
        if (stats.amusement > 100)
            stats.amusement = 99;
    }

    public void Face()
    {
        //-----------------[ Facial Expression Conditions ]--------------------

        //Asleep
        if (!stats.isAwake)
            emotionStateMachine.emotion = AI_EmotionState.Emotion.Asleep;
        else
        {
            //Tired
            if (stats.energy < stats.energyToTired || arduino.pointLights.activeSelf == false)
                emotionStateMachine.emotion = AI_EmotionState.Emotion.Tired;

            //Wondering
            else if (stateMachine.State == AI_StateMachine.state.Notice)
                emotionStateMachine.emotion = AI_EmotionState.Emotion.Wondering;


            //Happy
            else if (stats.happiness > stats.happinessToHappy)
                emotionStateMachine.emotion = AI_EmotionState.Emotion.Happy;

            //"Bored"
            else if (stats.happiness > stats.happinessToUpset && stats.amusement < stats.amusementToUpset)
                emotionStateMachine.emotion = AI_EmotionState.Emotion.Upset;

            //Default
            else if (stats.happiness > stats.happinessToNeural)
                emotionStateMachine.emotion = AI_EmotionState.Emotion.Default;

            //Default
            else if (stats.amusement > stats.amusementToNeutral && stats.happiness < stats.happinessToSad)
                emotionStateMachine.emotion = AI_EmotionState.Emotion.Default;

            //"Sad"
            else if (stats.happiness < stats.happinessToSad || stats.amusement < stats.amusementToSad)
                emotionStateMachine.emotion = AI_EmotionState.Emotion.Sad;
        }
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

        if (Input.GetKeyDown(KeyCode.Q))
            stopAllCoroutines = true;

        //Interact With Tamagochi
        if (Input.GetKeyDown(KeyCode.Space))
        {
            stats.attention += Random.Range(20, 35);
            aware.Aware();
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (!card.isGiven)
                card.cardInserted = true;
            else
                card.cardInserted = false;
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (arduino.pointLights.activeSelf)
                arduino.pointLights.SetActive(false);
            else
                arduino.pointLights.SetActive(true);
        }

    }

    public IEnumerator NewState()
    {
        stateMachine.State = AI_StateMachine.state.Default;
        yield return new WaitForSeconds(0.01f);
        canNewState = true;
    }

    public IEnumerator BadNight()
    {
        yield return new WaitForSeconds(1);
        stats.energy = 15;
        Debug.Log("askldasmdalkdmasdsad");
    }
}
