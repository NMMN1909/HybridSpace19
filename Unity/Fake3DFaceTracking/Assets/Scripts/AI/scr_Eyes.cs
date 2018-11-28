using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Eyes : MonoBehaviour {

    public GameObject head;
    public Transform toy;
    public Transform faceForward;
    public Transform screenPosEyes;

    //Reference
    public scr_Playing playing;
    public scr_ScreenTap screenTap;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (screenTap.isScreenTapping)
        {
            head.transform.LookAt(screenPosEyes.position);
        }
        else if (playing.isPlaying)
        {
            head.transform.LookAt(toy.position);
        }
        else
        {
            head.transform.LookAt(faceForward.position);
        }

    }
}
