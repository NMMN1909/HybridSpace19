using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Jumping : MonoBehaviour {

    private Rigidbody rb;
    public BoxCollider jumpCol;
    public bool onGround;
    public bool canJump;
    public bool isJumping;

    public float jumpForce;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        jumpForce = 5f;
	}
	
	// Update is called once per frame
	void Update () {

        // Jump BOOL
        if (canJump && isJumping)
        {
            PerformJump();
        }

    }

    private void PerformJump()
    {
        rb.velocity = Vector3.zero;
        this.GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        canJump = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            onGround = true;
            canJump = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            onGround = true;
            canJump = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            onGround = false;
            canJump = false;
        }
    }
}
