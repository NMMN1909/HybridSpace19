using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_glassScale : MonoBehaviour {

    // Initialize the public variables
    public Camera eyeCamera;

    private float size;

	// Use this for initialization
	void Start ()
    {
        size = transform.localScale.x;
	}
	
	// Update is called once per frame
	void Update ()
    {
        float fovPercentage = eyeCamera.fieldOfView / 179f;

        transform.localScale = new Vector3(size * fovPercentage, 1f, size * fovPercentage);
    }
}
