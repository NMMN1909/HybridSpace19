using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_ThinkingCloud : MonoBehaviour {

    public Transform followTarget;
    public Transform playerCamLoc;
    private Transform offsetLoc;

    public float offsetX;
    public float offsetY;
    public float offsetZ;
    public bool thinkingCloudBool;

    public Animator animController;

    private int thinkingCloudTimer;
    private int thinkingCloudCounter;

    public List<Sprite> thinkingCloud = new List<Sprite>();

    // Use this for initialization
    void Start () {
        offsetX = 1;
        offsetY = 2.5f;
        offsetZ = -1f;
        thinkingCloudBool = false;
        thinkingCloudTimer = 400;
        thinkingCloudCounter = 0;
    }
	
	// Update is called once per frame
	void Update () {
        this.transform.position = followTarget.transform.position + new Vector3(offsetX, offsetY, offsetZ);
        this.transform.LookAt(playerCamLoc);

        //Debug Play Fade Animation
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            this.GetComponent<SpriteRenderer>().sprite = thinkingCloud[1];
            thinkingCloudBool = true;
        }


        if(thinkingCloudCounter > thinkingCloudTimer)
        {
            thinkingCloudBool = true;
            thinkingCloudCounter = 0;
        }

        //Animator Controller
        if (thinkingCloudBool)
        {
            animController.SetBool("isThinkingCloud", true);
            thinkingCloudBool = false;
        }
        else
            animController.SetBool("isThinkingCloud", false);

        thinkingCloudCounter += 1;
    }

}
