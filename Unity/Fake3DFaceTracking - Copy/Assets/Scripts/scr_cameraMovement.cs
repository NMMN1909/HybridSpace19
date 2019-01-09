using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_cameraMovement : MonoBehaviour {

    // Initialize the public variables
    public float mouseSensitivityX;
    public float mouseSensitivityY;

    // Initialize the private variables
    private float rotationX;
    private float rotationY;
	
	// Update is called once per frame
	void Update ()
    {
        rotationX += (Input.GetAxis("Mouse Y") * mouseSensitivityY) * Time.deltaTime;
        rotationY -= (Input.GetAxis("Mouse X") * mouseSensitivityX) * Time.deltaTime;

        transform.rotation = Quaternion.Euler(rotationX, rotationY, transform.eulerAngles.z);
	}
}
