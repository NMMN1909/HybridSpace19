using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Battery : MonoBehaviour {

    public static float batteryLevel;

    // Use this for initialization
    void Start () {



	}
	
	// Update is called once per frame
	void Update () {

        batteryLevel = SystemInfo.batteryLevel;
        Debug.Log("Battery Level = " + batteryLevel);

	}
}
