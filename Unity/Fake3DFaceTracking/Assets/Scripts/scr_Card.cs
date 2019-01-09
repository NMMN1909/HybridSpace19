using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Card : MonoBehaviour {

    //References
    private Renderer rend;
    public GameObject card;
    public Animation animCardGive;

    public float animDuration;
    public bool isGiven;
    public bool canGive;
    public bool cardInserted;

    //Color
    private Color32 greenColor;
    private Color32 blueColor;
    private Color32 redColor;
    private Color32 yellowColor;
    private Color32 purpleColor;
    private Color32 cyanColor;

    // Use this for initialization
    void Start () {
        animDuration = 1f;
        canGive = true;
        isGiven = false;

        rend = GetComponent<Renderer>();

        blueColor = new Color32(36, 126, 217, 255);
        redColor = new Color32(217, 36, 36, 255);
        greenColor = new Color32(54, 130, 51, 255);
        yellowColor = new Color32(255, 196, 100, 255);
        purpleColor = new Color32(225, 0, 255, 255);
        cyanColor = new Color32(0, 255, 242, 255);
    }
	
	// Update is called once per frame
	void Update () {

        if (cardInserted && !isGiven && canGive)
        {
            canGive = false;

            //if(arduinoColor == X)
            rend.material.color = blueColor;

            animCardGive.Play("Card_Give");
            isGiven = true;
            StartCoroutine(InputDelay());
        }

        else if(!cardInserted && isGiven && canGive)
        {
            canGive = false;
            animCardGive.Play("Card_Take");
            isGiven = false;
            StartCoroutine(InputDelay());
        }
	}

    IEnumerator InputDelay()
    {
        yield return new WaitForSeconds(animDuration);
        canGive = true;
    }

}
