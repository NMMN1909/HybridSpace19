
using UnityEngine;
using System;
using System.Collections;
using System.Threading;
using System.IO.Ports;

public class Script_Arduino : MonoBehaviour 
{
    //Reference
    public scr_Card card;


    SerialPort stream = new SerialPort("COM3", 9600);
	int Timer;
	string distance;
	Boolean InputSwitch;
	Boolean TimerBool;
    bool isInteracting;

    int ignoreTime = 1;

    public scr_playerStats playerStats;
    public GameObject pointLights;
    public GameObject player;

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
        if (player.GetComponent<scr_playerStats>().playerState == scr_playerStats.states.Interact)
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
        if (id == 9 || id == 10)
            card.cardInserted = true;

        Debug.Log(id);

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
                if (playerStats.isAwake)
                    playerStats.playerState = scr_playerStats.states.Respond;
                break;

            // 
            case 8:
                card.cardInserted = false;
                break;

            // Groene kaart
            case 9:
                if (playerStats.playerState == scr_playerStats.states.Interact)
                    playerStats.playerState = scr_playerStats.states.Grow;
                break;

            // Blauwe kaart
            case 10:
                if (playerStats.playerState == scr_playerStats.states.Interact)
                    playerStats.playerState = scr_playerStats.states.Colorize;
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