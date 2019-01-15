using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_playerGrounded : MonoBehaviour {

    public bool grounded;
	
	// Update is called once per frame
	void Update () {
        RaycastHit onGroundCheck;
        Vector3 physicsCentre = this.transform.position + this.GetComponent<CapsuleCollider>().center;

        Debug.DrawRay(physicsCentre, Vector3.down * 6f, Color.red, 1);
        if (Physics.Raycast(physicsCentre, Vector3.down, out onGroundCheck, 55f))
        {
            if (onGroundCheck.transform.gameObject.tag != "Player")
                grounded = true;
        }
        else
        {
            grounded = false;
        }
    }
}
