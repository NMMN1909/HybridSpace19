using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_ball : MonoBehaviour
{
    // Initialize the public variables
    public Transform playerTransform;
    public AI_StateMachine stateMachine;
    public float force;
    public AudioSource audioSource;
    public Animator animController;

    // Initialize the private variables
    Rigidbody rigidBody;

	// Use this for initialization
	void Start ()
    {
        rigidBody = GetComponent<Rigidbody>();
	}
	
    void OnCollisionEnter(Collision collision)
    {
        if (stateMachine.State == AI_StateMachine.state.Playing)
        {
            animController.SetTrigger("isPlaying");
            rigidBody.AddForce(playerTransform.forward * force);

            if (!audioSource.isPlaying)
                audioSource.Play();
        } 
    }
}
