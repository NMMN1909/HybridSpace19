using System.Collections;
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
                break;

            // The wake playerState
            case scr_playerStats.states.Wake:
                playerFunctions.thinking();
                playerFunctions.wake();
                break;

            // The sleep playerState
            case scr_playerStats.states.Sleep:
                playerFunctions.thinking();
                playerFunctions.sleep();
                break;

            // The playing playerState
            case scr_playerStats.states.Playing:
                playerFunctions.thinking();
                playerFunctions.playing();
                break;

            case scr_playerStats.states.Grow:
                playerFunctions.grow(.0005f, .225f);
                break;

            case scr_playerStats.states.Colorize:
                playerFunctions.colorize();
                break;

            case scr_playerStats.states.Float:
                playerFunctions.fly();
                break;
        }

        DebugController();
    }

    // Controls the debug
    private void DebugController()
    {
        if (Input.GetKeyDown(KeyCode.Z))
            playerStats.energy -= 10f;

        if (Input.GetKeyDown(KeyCode.X))
            playerStats.happiness += 10f;

        if (Input.GetKeyDown(KeyCode.C))
            playerStats.amusement += 10f;

        if (Input.GetKeyDown(KeyCode.V))
            playerStats.affection += 10f;

        if (Input.GetKeyDown(KeyCode.A))
        {
            playerStats.playerState = scr_playerStats.states.Idle;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            playerStats.playerState = scr_playerStats.states.Respond;
        }

        if (Input.GetKeyDown(KeyCode.Q))
            playerStats.playerState = scr_playerStats.states.Float;
    }
}
