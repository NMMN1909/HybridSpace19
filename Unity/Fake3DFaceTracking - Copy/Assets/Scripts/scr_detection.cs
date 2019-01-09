using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_detection : MonoBehaviour {

    //References
    public scr_playerStats playerStats;
    private CapsuleCollider coll;

	// Use this for initialization
	void Start () {
        coll = GetComponent<CapsuleCollider>();
	}
	
    private void OnTriggerEnter(Collider other)
    {
        if(playerStats.playerState != scr_playerStats.states.Respond)
        {
            if (other.tag == "Drawer")
            {
                playerStats.wanderingDirection = Random.Range(90f, 270f);
            }
        }

        if (other.tag == "Wall")
        {
            playerStats.wanderingDirection = Random.Range(90f, 270f);
        }
    }
}
