using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_TamagochiAI_v2 : MonoBehaviour {

    //Reference
    private Renderer rend;
    public scr_Roaming roaming;
    public scr_Jumping jumping;
    public scr_TamagochiVision vision;
    public scr_Playing playing;
    public scr_Sleep sleeping;
    public scr_ScreenTap screenTap;

    //Color
    private Color32 greenColor;
    private Color32 blueColor;
    private Color32 redColor;
    private Color32 yellowColor;
    private Color32 purpleColor;
    private Color32 cyanColor;

    public float roamingDuration;
    public float idleDuration;
    public float jumpDuration;
    public float playDuration;
    public float sleepDuration;
    public float screenTapDuration;

    public float rotationAngle;

    private bool doOnce;

    public enum TamagochiState
    {
        IDLE,
        ROAMING,
        PLAYING,
        JUMPING,
        SLEEPING,
        SCREENTAP,
    }
    TamagochiState tamagochiState = TamagochiState.PLAYING;

    void Start()
    {
        idleDuration = 2f;
        roamingDuration = 8f;
        jumpDuration = 3f;
        playDuration = 6f;
        sleepDuration = 2f;
        screenTapDuration = 4f;

        rotationAngle = 90f;

        doOnce = true;

        blueColor = new Color32(36, 126, 217, 255);
        redColor = new Color32(217, 36, 36, 255);
        greenColor = new Color32(54, 130, 51, 255);
        yellowColor = new Color32(255, 196, 100, 255);
        purpleColor = new Color32(225, 0, 255, 255);
        cyanColor = new Color32(0, 255, 242, 255);

        rend = GetComponentInChildren<Renderer>();
    }


    void Update()
    {
        if (vision.hitWall && roaming.isRoaming)
        {
            //Turn Around
            transform.Rotate(Vector3.up * roaming.rotSpeed);
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

            case TamagochiState.JUMPING:

                rend.material.color = yellowColor;
                Debug.Log(TamagochiState.JUMPING);
                if (doOnce)
                {
                    StartCoroutine(JumpingCoroutine());
                    doOnce = false;
                }
                break;


            case TamagochiState.PLAYING:

                rend.material.color = redColor;
                Debug.Log(TamagochiState.PLAYING);
                if (doOnce)
                {
                    StartCoroutine(PlayingCoroutine());
                    doOnce = false;
                }
                break;

            case TamagochiState.SLEEPING:

                rend.material.color = purpleColor;
                Debug.Log(TamagochiState.SLEEPING);
                if (doOnce)
                {
                    StartCoroutine(SleepingCoroutine());
                    doOnce = false;
                }
                break;

            case TamagochiState.SCREENTAP:

                rend.material.color = cyanColor;
                Debug.Log(TamagochiState.SCREENTAP);
                if (doOnce)
                {
                    StartCoroutine(ScreenTapCoroutine());
                    doOnce = false;
                }
                break;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            doOnce = true;
            if (doOnce)
            {
                StopAllCoroutines();
                tamagochiState = TamagochiState.SCREENTAP;
                roaming.roamBool = false;
                jumping.isJumping = false;
                playing.isPlaying = false;
                sleeping.isSleeping = false;
                doOnce = false;
                Debug.Log("WTF IS THIS");
            }
            doOnce = true;
        }
    }


    public IEnumerator IdleCoroutine()
    {
        tamagochiState = TamagochiState.IDLE;
        yield return new WaitForSeconds(idleDuration);

        RandomState();
        doOnce = true;
    }

    public IEnumerator RoamingCoroutine()
    {
        tamagochiState = TamagochiState.ROAMING;
        roaming.roamBool = true;
        yield return new WaitForSeconds(roamingDuration);
        roaming.roamBool = false;

        RandomState();
        doOnce = true;
    }

    public IEnumerator JumpingCoroutine()
    {
        tamagochiState = TamagochiState.JUMPING;
        jumping.isJumping = true;
        yield return new WaitForSeconds(jumpDuration);
        jumping.isJumping = false;

        RandomState();
        doOnce = true;
    }

    public IEnumerator PlayingCoroutine()
    {
        tamagochiState = TamagochiState.PLAYING;
        playing.isPlaying = true;
        yield return new WaitForSeconds(playDuration);
        playing.isPlaying = false;

        RandomState();
        doOnce = true;
    }

    public IEnumerator SleepingCoroutine()
    {
        tamagochiState = TamagochiState.SLEEPING;
        sleeping.isSleeping = true;
        yield return new WaitForSeconds(sleepDuration);
        sleeping.isSleeping = false;

        RandomState();
        doOnce = true;
    }

    public IEnumerator ScreenTapCoroutine()
    {
        tamagochiState = TamagochiState.SCREENTAP;
        screenTap.isScreenTapping = true;
        yield return new WaitForSeconds(screenTapDuration);
        screenTap.isScreenTapping = false;

        RandomState();
        doOnce = true;
    }

    private void RandomState()
    {
        int randomState = Random.Range(0, 6);
        if(randomState == 0)
        {
            tamagochiState = TamagochiState.IDLE;
        }
        else if(randomState == 1)
        {
            tamagochiState = TamagochiState.ROAMING;
        }
        else if (randomState == 2)
        {
            tamagochiState = TamagochiState.PLAYING;
        }
        else if (randomState == 3)
        {
            tamagochiState = TamagochiState.JUMPING;
        }
        else if (randomState == 4)
        {
            tamagochiState = TamagochiState.SLEEPING;
        }
        else if (randomState == 5)
        {
            tamagochiState = TamagochiState.PLAYING;
        }
    }

}
