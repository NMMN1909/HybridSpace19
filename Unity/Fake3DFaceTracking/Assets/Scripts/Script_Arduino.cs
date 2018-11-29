
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


	void ScreenTik (int Tik)
	{
        if (Tik == 1)
        {
            Debug.Log("Lampje");

            if (pointLights.activeSelf)
                pointLights.SetActive(false);
            else
                pointLights.SetActive(true);
        }

		if (Tik == 2)
		{
            Debug.Log("Tik");
            playerStats.playerState = scr_playerStats.states.Respond;
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