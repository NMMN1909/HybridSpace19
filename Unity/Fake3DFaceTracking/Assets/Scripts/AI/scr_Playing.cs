using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Playing : MonoBehaviour {

    //Playing Stats
    public Transform target;
    public float chaseSpeed;
    public float chaseRotSpeed;
    const float EPSILON = 0.1f;
    public bool isPlaying;
    public Transform head;
    public Transform faceForward;
    public scr_ScreenTap screen;


    // Use this for initialization
    void Start () {
        chaseSpeed = 3f;
        chaseRotSpeed = 30f;
	}
	
	// Update is called once per frame
	void Update () {

        if (isPlaying)
        {
            Playing();
        }
	}

    private void Playing()
    {
        if ((transform.position - target.position).magnitude > EPSILON)
        {
            //move towards the player
            //transform.position += transform.forward * chaseSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.position.x, 0f, target.position.z), .025f);
        }



        ////rotate to look at the player
        //transform.rotation = Quaternion.Slerp(transform.rotation,
        //Quaternion.LookRotation(target.position - transform.position), chaseRotSpeed * Time.deltaTime);
    }
}
