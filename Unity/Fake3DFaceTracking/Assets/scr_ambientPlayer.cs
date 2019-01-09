using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_ambientPlayer : MonoBehaviour
{
    // Initialize the public variables
    public AudioSource audioSource;
    public AudioClip[] audioClip;
    public float alarmDurationMin;
    public float alarmDurationMax;

	// Update is called once per frame
	void Start ()
    {
        StartCoroutine(Alarm());
    }

    IEnumerator Alarm ()
    {
        yield return new WaitForSeconds(Random.Range(alarmDurationMin, alarmDurationMax));
        audioSource.clip = audioClip[Mathf.RoundToInt(Random.Range(0f, audioClip.Length - 1f))];
        audioSource.Play();
        StartCoroutine(Alarm());
    }
}
