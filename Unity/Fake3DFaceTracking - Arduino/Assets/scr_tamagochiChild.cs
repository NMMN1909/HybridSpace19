using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_tamagochiChild : MonoBehaviour
{
    // Initialize the public variables
    public float duration;
    public Transform spawnTransform;

    public void startRoutine()
    {
        StartCoroutine(DestroyCycle());
    }
	
    IEnumerator DestroyCycle()
    {
        yield return new WaitForSeconds(duration);
        transform.position = spawnTransform.position;
        transform.rotation = Quaternion.Euler(spawnTransform.rotation.x, spawnTransform.rotation.y, spawnTransform.rotation.z);
        this.gameObject.SetActive(false);
    }
}
