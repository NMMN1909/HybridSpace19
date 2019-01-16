using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_StateMachine : MonoBehaviour {

    //public enum states { Idle, Wandering, Respond, Interact, Annoy, Wake, Sleep, Playing, Grow, Colorize };
    public enum state {Default, Roaming, Notice, Respond, Wake, Sleep, Interact, Upset, WindowSlam, Playing, ReadCard, Float, Grow, Colorize, Duplicate};

    //Reference
    private AI_Idle idle;
    private AI_Controller controller;
    private AI_Roaming roaming;
    private AI_Variables stats;
    private AI_Notice notice;
    private AI_Respond respond;
    private AI_Wake wake;
    private AI_Sleep sleep;
    private AI_Interact interact;
    private AI_Upset upset;
    private AI_WindowSlam windowSlam;
    private AI_Playing playing;
    private AI_Grow grow;
    private AI_Colorize colorize;
    private AI_ReadCard readCard;
    private AI_duplicate duplicate;

    // Initialize the public variables
    public state State;

    // Run this code every frame
    void Update()
    {
        // Scroll through the different playerStates and run the appropriate functions
        switch (State)
        {
            // The idle playerState
            case state.Default:
                controller.Brain();
                controller.Senses();
                controller.Cells();
                controller.Face();
                idle.Idle();
                break;

            // The wandering playerState
            case state.Roaming:
                controller.Brain();
                controller.Senses();
                controller.Cells();
                controller.Face();
                roaming.Roaming();
                break;

            // The roaming playerState
            case state.Notice:
                controller.Brain();
                controller.Senses();
                controller.Cells();
                controller.Face();
                notice.Notice();
                break;

            // The respond playerState
            case state.Respond:
                controller.Brain();
                controller.Senses();
                controller.Cells();
                controller.Face();
                respond.Respond();
                break;

            // The upset playerState
            case state.Wake:
                controller.Brain();
                controller.Senses();
                controller.Cells();
                controller.Face();
                wake.Wake();

                break;

            // The sleep playerState
            case state.Sleep:
                controller.Brain();
                controller.Senses();
                controller.Cells();
                controller.Face();
                sleep.Sleep();
                break;

            // The interact playerState
            case state.Interact:
                controller.Brain();
                controller.Senses();
                controller.Cells();
                controller.Face();
                interact.Interact();
                break;

            // The upset playerState
            case state.Upset:
                controller.Brain();
                controller.Senses();
                controller.Cells();
                controller.Face();
                upset.Upset();
                break;

            // The upset playerState
            case state.WindowSlam:
                controller.Brain();
                controller.Senses();
                controller.Cells();
                controller.Face();
                windowSlam.WindowSlam();
                break;

            // The upset playerState
            case state.Grow:
                controller.Senses();
                controller.Face();
                grow.Grow(.05f, 18f); //
                break;

            // The upset playerState
            case state.Colorize:
                controller.Senses();
                controller.Face();
                colorize.Colorize();
                break;

            // The playing playerState
            case state.Playing:
                controller.Brain();
                controller.Senses();
                controller.Cells();
                controller.Face();
                break;

            // The readcard playerState
            case state.ReadCard:
                controller.Senses();
                controller.Face();
                readCard.ReadCard();
                break;

            // The duplicate playerState
            case state.Duplicate:
                controller.Senses();
                controller.Face();
                duplicate.Duplicate();
                break;
        }
    }

    private void Start()
    {
        idle = GetComponent<AI_Idle>();
        controller = GetComponent<AI_Controller>();
        roaming = GetComponent<AI_Roaming>();
        notice = GetComponent<AI_Notice>();
        respond = GetComponent<AI_Respond>();
        wake = GetComponent<AI_Wake>();
        sleep = GetComponent<AI_Sleep>();
        interact = GetComponent<AI_Interact>();
        upset = GetComponent<AI_Upset>();
        windowSlam = GetComponent<AI_WindowSlam>();
        playing = GetComponent<AI_Playing>();
        grow = GetComponent<AI_Grow>();
        colorize = GetComponent<AI_Colorize>();
        readCard = GetComponent<AI_ReadCard>();
        duplicate = GetComponent<AI_duplicate>();
    }
}
