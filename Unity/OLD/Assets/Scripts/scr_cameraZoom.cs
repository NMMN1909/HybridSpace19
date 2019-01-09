using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_cameraZoom : MonoBehaviour {

    // Initialize the public variables
    public float zoomSpeed;

	// Update is called once per frame
	void Update ()
    {
        transform.position += ((transform.forward * zoomSpeed) * Input.GetAxis("Mouse ScrollWheel")) * Time.deltaTime;
	}
}
