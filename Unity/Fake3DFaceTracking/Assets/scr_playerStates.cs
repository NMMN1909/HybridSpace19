﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_playerStates : MonoBehaviour
{
    // Initialize the public variables
    public scr_playerStats playerStats;
    public scr_playerFunctions playerFunctions;

    // Run this code every frame
    void Update()
    {
        // Scroll through the different playerStates and run the appropriate functions
        switch (playerStats.playerState)
        {
            // The idle playerState
            case scr_playerStats.states.Idle:
                playerFunctions.thinking();
                playerFunctions.idle(playerStats.idleDurationMin, playerStats.idleDurationMax);
                break;

            // The wandering playerState
            case scr_playerStats.states.Wandering:
                playerFunctions.thinking();
                playerFunctions.wandering(playerStats.movementSpeed, playerStats.wanderingDurationMin, playerStats.wanderingDurationMax);
                break;

            // The respond playerState
            case scr_playerStats.states.Respond:
                playerFunctions.respond(playerStats.movementSpeed);
                break;

            // The interact playerState
            case scr_playerStats.states.Interact:
                playerFunctions.interact();
                break;

            // The sleep playerState
            case scr_playerStats.states.Annoy:
                playerFunctions.thinking();
                playerFunctions.annoy();
                break;


            // The sleep playerState
            case scr_playerStats.states.Wake:
                playerFunctions.thinking();
                playerFunctions.wake();
                break;

            // The sleep playerState
            case scr_playerStats.states.Sleep:
                playerFunctions.thinking();
                playerFunctions.sleep();
                break;

            // The sleep playerState
            case scr_playerStats.states.Playing:
                playerFunctions.thinking();
                playerFunctions.playing();
                break;
        }
    }
}
