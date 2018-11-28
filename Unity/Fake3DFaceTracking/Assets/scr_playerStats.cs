using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_playerStats : MonoBehaviour
{
    // 
    public enum states { Thinking, Idle, Wandering, Respond, Interact };

    // Initialize the public variables
    public states playerState;
    public float movementSpeed;
    public float wanderingDurationMin;
    public float wanderingDurationMax;
    public float idleDurationMin;
    public float idleDurationMax;
}
