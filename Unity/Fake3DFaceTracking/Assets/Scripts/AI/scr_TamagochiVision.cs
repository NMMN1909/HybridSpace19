using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_TamagochiVision : MonoBehaviour {

    private Transform direction;

    public float rayCastRange;
    public RaycastHit hitFront;
    public bool hitWall;

    // Use this for initialization
    void Start () {
        direction = transform;
        rayCastRange = 1f;
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 physicsCentre = (this.transform.position + this.GetComponent<CapsuleCollider>().center) + new Vector3(0, 0, 0);
        Debug.DrawRay(physicsCentre + new Vector3(0, -.5f, 0), Vector3.forward * rayCastRange, Color.yellow, rayCastRange);//draws a raycast to visualy see wheither or not the player is on the ground

        if (Physics.Raycast((transform.position - new Vector3(0, 0, 0)), transform.forward, out hitFront, rayCastRange))
        {
            if(hitFront.transform.tag != "Tamagochi")
            {
                if (hitFront.transform.tag == "Wall")
                {
                    hitWall = true;
                }
            }


        }
        else
        {
            hitWall = false;
        }
        Debug.Log(hitWall);
        



    }
}
