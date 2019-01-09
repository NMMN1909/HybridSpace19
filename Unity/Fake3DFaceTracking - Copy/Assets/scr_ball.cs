using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_ball : MonoBehaviour
{
    // Initialize the public variables
    public Transform playerTransform;
    public scr_playerStats playerStats;
    public float force;

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
        if (playerStats.playerState == scr_playerStats.states.Playing)
            rigidBody.AddForce(playerTransform.forward * force);
    }
}
