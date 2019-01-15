﻿
using UnityEngine;
using System;
using System.Collections;
using System.Threading;
using System.IO.Ports;

public class Script_Arduino : MonoBehaviour 
{
    //Reference
    public scr_Card card;
    public AI_StateMachine stateMachine;
    public AI_Variables stats;
    public GameObject pointLights;
    public GameObject player;

    SerialPort stream = new SerialPort("COM3", 9600);
	int Timer;
	string distance;
	Boolean InputSwitch;
	Boolean TimerBool;
    bool isInteracting;

    int ignoreTime = 1;

    string str;

	void Start() 
	{
        stream.Open(); //Open the Serial Stream.\
		stream.DataReceived += DataReceivedHandler;
		//stream.BaseStream.ReadTimeout = 2;
		stream.ReadTimeout = 1;
		stream.WriteTimeout = 1;

		Timer = 0;
		InputSwitch = false;
		TimerBool = false;
    }

	void Update()
	{
        if (stateMachine.State == AI_StateMachine.state.Interact)
        {
            if (!isInteracting)
            {
                stream.Write("1");
                Debug.Log("Light On");
                isInteracting = true;
            }
        }
        else
        {
            if (isInteracting)
            {
                stream.Write("0");
                Debug.Log("Light Off");
                isInteracting = false;
            } 
        }

	    if (stream.IsOpen)
        {
			try
			{
				ScreenTik(stream.ReadByte());
			}
			catch (System.Exception)
			{
            }
        }
	}


	void ScreenTik (int id)
	{
        Debug.Log(id);
        if (id == 9 || id == 10)
            card.cardInserted = true;


        switch (id)
        {
            // Lampje
            case 1:
                if (pointLights.activeSelf)
                    pointLights.SetActive(false);
                else
                    pointLights.SetActive(true);
                break;

            // Tikken
            case 2:
                if (stats.isAwake)
                    stateMachine.State = AI_StateMachine.state.Respond;
                break;

            //
            case 8:
                card.cardInserted = false;
                break;

            // Groene kaart
            case 9:
                if (stateMachine.State == AI_StateMachine.state.Interact)
                    stateMachine.State = AI_StateMachine.state.Grow;
                break;

            // Blauwe kaart
            case 10:
                if (stateMachine.State == AI_StateMachine.state.Interact)
                    stateMachine.State = AI_StateMachine.state.Colorize;
                break;

            // Roze kaart
            case 11:
                break;

            // Paarse kaart
            case 12:
                break;

            // Slider position
            case 13:
                break;

            case 14:
                break;

            case 15:
                break;

            case 16:
                break;

            case 17:
                break;
        }
	}

	private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
     {
         SerialPort sp = (SerialPort)sender;
         string distance = sp.ReadLine();
         Debug.Log(distance);
     }
}