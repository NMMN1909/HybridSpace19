using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Roaming : MonoBehaviour {

    //Reference
    private Rigidbody rb;
    public scr_TamagochiVision vision;

    public float movSpeed;
    public float rotSpeed;

    public bool isNewWanderLoc = false;
    public bool isRotatingLeft = false;
    public bool isRotatingRight = false;
    public bool isRoaming = true;
    public bool roamBool;

    //public Vector3 movForward;
    //public Vector3 movement;


	// Use this for initialization
	void Start () {

        movSpeed = 1f;
        rotSpeed = 100f;
        rb = GetComponent<Rigidbody>();
        isNewWanderLoc = true;

	}

    private void Update()
    {
        if (roamBool)
        {
            if (isNewWanderLoc)
            {
                StartCoroutine(Roaming());
            }

            if (isRotatingRight)
            {
                transform.Rotate(transform.up * Time.deltaTime * rotSpeed);
            }
            if (isRotatingLeft)
            {
                transform.Rotate(transform.up * Time.deltaTime * -rotSpeed);
            }


            if (isRoaming)
            {
                transform.position += transform.forward * Time.deltaTime * movSpeed;
            }
        }



    }

    private void FixedUpdate()
    {
        //movForward = transform.forward * 1 * Time.deltaTime;
        //movement = movForward * movSpeed;
        //movement = Vector3.forward * movSpeed * Time.deltaTime;
    }

    //public void Roaming()
    //{

        //if (vision.hitWall)
        //{
        //    transform.Rotate(Vector3.up * Time.deltaTime * rotSpeed);
        //}
        //else
        //{
            
        //    //movement.y = rb.velocity.y;
        //    //rb.velocity = movement;
        //    transform.Translate(movement * 1);

        //}

    //}

    public IEnumerator Roaming()
    {
        int rotDuration = Random.Range(1, 3);
        int rotatePause = Random.Range(1, 4);
        int rotateLorR = Random.Range(0, 3);
        int roamPause = Random.Range(1, 4);
        int roamDuration = Random.Range(1, 5);

        isNewWanderLoc = false;

        yield return new WaitForSeconds(roamPause);
        isRoaming = true;
        yield return new WaitForSeconds(roamDuration);
        isRoaming = false;
        yield return new WaitForSeconds(rotatePause);
        if(rotateLorR == 1)
        {
            isRotatingRight = true;
            yield return new WaitForSeconds(rotDuration);
            isRotatingRight = false;
        }
        if (rotateLorR == 2)
        {
            isRotatingLeft = true;
            yield return new WaitForSeconds(rotDuration);
            isRotatingLeft = false;
        }
        isNewWanderLoc = true;
    }
}
