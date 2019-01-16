using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Audio : MonoBehaviour {

    //Reference
    public AudioSource audioSource;
    public AudioClip[] audioClip;
    private AI_StateMachine stateMachine;
    private AI_Roaming roaming;

    private bool doOnce;

    // Use this for initialization
    void Start () {
        stateMachine = GetComponent<AI_StateMachine>();
        roaming = GetComponent<AI_Roaming>();
        doOnce = true;
	}
	
	// Update is called once per frame
	void Update () {
        if(stateMachine.State == AI_StateMachine.state.Roaming && roaming.canMovForward && doOnce)
        {
            StartCoroutine(JumpSoundCycle());
            doOnce = false;
        }

        if((stateMachine.State == AI_StateMachine.state.Respond || stateMachine.State == AI_StateMachine.state.Upset || stateMachine.State == AI_StateMachine.state.Playing) && doOnce)
        {
            StartCoroutine(JumpingSoundCycle());
            doOnce = false;
        }

        if (stateMachine.State == AI_StateMachine.state.WindowSlam && doOnce)
        {
            StartCoroutine(WindowSlamSoundCycle());
            doOnce = false;
        }
    }

    IEnumerator JumpSoundCycle()
    {
        StopCoroutine(JumpingSoundCycle());
        StopCoroutine(WindowSlamSoundCycle());
        yield return new WaitForSeconds(.8f);
        audioSource.clip = audioClip[1];
        audioSource.Play();
        doOnce = true;
    }

    IEnumerator JumpingSoundCycle()
    {
        StopCoroutine(JumpSoundCycle());
        StopCoroutine(WindowSlamSoundCycle());
        yield return new WaitForSeconds(.915f);
        audioSource.clip = audioClip[1];
        audioSource.Play();
        doOnce = true;
    }

    IEnumerator WindowSlamSoundCycle()
    {
        StopCoroutine(JumpingSoundCycle());
        StopCoroutine(JumpSoundCycle());
        yield return new WaitForSeconds(.9f);
        audioSource.clip = audioClip[2];
        audioSource.Play();
        doOnce = true;
    }
}
