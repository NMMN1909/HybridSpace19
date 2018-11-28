using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Toy : MonoBehaviour {

    public float toyMovSpeed;
    public float toyRotSpeed;

	// Use this for initialization
	void Start () {

        toyMovSpeed = 3f;
        toyRotSpeed = 60f;

	}
	
	// Update is called once per frame
	void Update () {

        transform.Translate(0, 0, toyMovSpeed * Time.deltaTime);
        transform.Rotate(0, -toyRotSpeed * Time.deltaTime, 0);

    }
}
