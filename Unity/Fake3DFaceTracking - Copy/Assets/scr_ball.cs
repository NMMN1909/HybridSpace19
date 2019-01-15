using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_ball : MonoBehaviour
{
    // Initialize the public variables
    public Transform playerTransform;
    public AI_StateMachine playerStats;
    public float force;
    public AudioSource audioSource;

    // Initialize the private variables
    Rigidbody rigidBody;

	// Use this for initialization
	void Start ()
    {
        rigidBody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnCollisionEnter(Collision collision)
    {
        if (playerStats.State == AI_StateMachine.state.Playing)
        {
            rigidBody.AddForce(playerTransform.forward * force);

            if (!audioSource.isPlaying)
                audioSource.Play();
        } 
    }
}
