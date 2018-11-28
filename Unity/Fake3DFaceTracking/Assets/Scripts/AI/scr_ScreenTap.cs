using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_ScreenTap : MonoBehaviour {

    public Transform screenTap;
    public Transform tamagochi;
    public Transform head;
    public Transform faceForward;
    const float EPSILON = 0.1f;

    public scr_Playing play;

    public bool isScreenTapping;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

            if (isScreenTapping)
            {
                Called();
            }
	}

    private void Called()
    {

        if ((transform.position - screenTap.position).magnitude > EPSILON)
        {
            //transform.position += transform.forward * chaseSpeed * Time.deltaTime;
        }

        //rotate to look at the player
        //transform.rotation = Quaternion.Slerp(transform.rotation,
        //Quaternion.LookRotation(screenTap.position - transform.position), play.chaseRotSpeed * Time.deltaTime);

        transform.position = Vector3.MoveTowards(transform.position, new Vector3(screenTap.position.x, 0f, screenTap.position.z), .025f);
    }
}
