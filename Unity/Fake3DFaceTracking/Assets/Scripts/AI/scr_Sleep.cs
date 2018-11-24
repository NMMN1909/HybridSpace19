using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Sleep : MonoBehaviour {

    public CapsuleCollider Player;
    public bool isSleeping;
    public GameObject eyes;


	// Update is called once per frame
	void Update () {

        if (isSleeping)
        {
            Player.height = 0.7f;
            eyes.gameObject.SetActive(false);
        }
        else
        {
            Player.height = 2f;
            eyes.gameObject.SetActive(true);
        }

    }

}
