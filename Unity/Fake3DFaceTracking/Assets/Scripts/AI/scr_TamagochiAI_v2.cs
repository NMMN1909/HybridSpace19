using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_TamagochiAI_v2 : MonoBehaviour {

    //Reference
    private Renderer rend;
    public scr_Roaming roaming;
    public scr_Jumping jumping;
    public scr_TamagochiVision vision;

    //Color
    private Color32 greenColor;
    private Color32 blueColor;
    private Color32 redColor;
    private Color32 yellowColor;


    public float roamingDuration;
    public float idleDuration;
    public float jumpDuration;
    public float rotationAngle;

    private bool doOnce;

    public enum TamagochiState
    {
        IDLE,
        ROAMING,
        CHASING,
        JUMPING,
        SLEEPING,
    }
    TamagochiState tamagochiState = TamagochiState.ROAMING;

    void Start()
    {
        idleDuration = 5f;
        roamingDuration = 15f;
        jumpDuration = 8f;
        rotationAngle = 90f;

        doOnce = true;

        blueColor = new Color32(36, 126, 217, 255);
        redColor = new Color32(217, 36, 36, 255);
        greenColor = new Color32(54, 130, 51, 255);
        yellowColor = new Color32(255, 196, 100, 255);

        rend = GetComponentInChildren<Renderer>();
    }


    void Update()
    {

        if (vision.hitWall && roaming.isRoaming)
        {
            StopAllCoroutines();
            //Turn Around
            transform.Rotate(Vector3.up * roaming.rotSpeed);
        }
        else
        {

        }




        switch (tamagochiState)
        {
            case TamagochiState.IDLE:

                rend.material.color = blueColor;
                Debug.Log(TamagochiState.IDLE);
                if (doOnce)
                {
                    StartCoroutine(IdleCoroutine());
                    doOnce = false;
                }



                break;

            case TamagochiState.ROAMING:

                rend.material.color = greenColor;
                Debug.Log(TamagochiState.ROAMING);
                if (doOnce)
                {
                    StartCoroutine(RoamingCoroutine());
                    doOnce = false;
                }



                break;

            case TamagochiState.CHASING:


                break;



            case TamagochiState.JUMPING:

                rend.material.color = yellowColor;
                Debug.Log(TamagochiState.JUMPING);
                if (doOnce)
                {
                    StartCoroutine(JumpingCoroutine());
                    doOnce = false;
                }



                break;

            case TamagochiState.SLEEPING:




                break;
        }

    }


    public IEnumerator IdleCoroutine()
    {
        tamagochiState = TamagochiState.IDLE;
        yield return new WaitForSeconds(idleDuration);

        tamagochiState = TamagochiState.ROAMING;
        doOnce = true;
    }

    public IEnumerator RoamingCoroutine()
    {
        tamagochiState = TamagochiState.ROAMING;
        roaming.roamBool = true;
        yield return new WaitForSeconds(roamingDuration);
        roaming.roamBool = false;

        tamagochiState = TamagochiState.JUMPING;
        doOnce = true;
    }

    public IEnumerator JumpingCoroutine()
    {
        tamagochiState = TamagochiState.JUMPING;
        jumping.isJumping = true;
        yield return new WaitForSeconds(jumpDuration);
        jumping.isJumping = false;

        tamagochiState = TamagochiState.IDLE;
        doOnce = true;
    }

}
