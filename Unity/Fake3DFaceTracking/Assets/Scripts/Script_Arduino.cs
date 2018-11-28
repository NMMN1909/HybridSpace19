
using UnityEngine;
using System;
using System.Collections;
using System.Threading;
using System.IO.Ports;

public class Script_Arduino : MonoBehaviour 
{
	SerialPort stream = new SerialPort("COM3", 9600);
	int Timer;
	string distance;
	Boolean InputSwitch;
	Boolean TimerBool;

    int ignoreTime = 1;

    public scr_playerStats playerStats;
    public GameObject pointLights;

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

		//Debug.Log(Timer);

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

		if (InputSwitch == true)
		{
			Timer += 1;
	 		//Debug.Log(Timer);
		}

		if (Timer == 1)
		{
			TimerBool = true;
		}

		if (Timer > 1)
		{
			TimerBool = false;
		}

		if (Timer == 200)
		{
			Timer = 0;
			Debug.Log("Timer Finished");
			InputSwitch = false;
		}

		if (TimerBool == true)
		{
			//Debug.Log("Tik");
		}

		//Placeholder
	}


	void ScreenTik (int Tik)
	{
		if (Tik == 1)
		{
            if (!pointLights.activeSelf)
                pointLights.SetActive(true);
            else
                pointLights.SetActive(false);
		}

		if (Tik == 2 && InputSwitch == false)
		{
            if (ignoreTime <= 0)
            {
                Debug.Log("Tik");

                if (playerStats.playerState != scr_playerStats.states.Respond && playerStats.playerState != scr_playerStats.states.Interact)
                    playerStats.playerState = scr_playerStats.states.Respond;
            }

            ignoreTime--;

			InputSwitch = true;
			
		}
	}

	private void DataReceivedHandler(
                         object sender,
                         SerialDataReceivedEventArgs e)
     {
         SerialPort sp = (SerialPort)sender;
         string distance = sp.ReadLine();
         Debug.Log(distance);
     }
}