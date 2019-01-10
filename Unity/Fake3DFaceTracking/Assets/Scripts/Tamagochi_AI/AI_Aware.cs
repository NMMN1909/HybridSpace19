using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Aware : MonoBehaviour {

    //Reference
    private AI_Variables stats;
    public Transform head;
    public Transform tamagochiHead;
    private AI_Controller controller;

    private Animator anim;
    public Transform tamaHead;
    public Vector3 Offset;
    public bool headRotLimit;

    private bool canAware;
    public bool isAware;

    private float angle;

    // Use this for initialization
    void Start () {
        stats = GetComponent<AI_Variables>();
        controller = GetComponent<AI_Controller>();
        canAware = true;

        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        AwareCheck();
        if (isAware)
            tamagochiHead.transform.rotation = Quaternion.Slerp(tamagochiHead.transform.rotation, Quaternion.LookRotation(head.position), .5f);
        else
            tamagochiHead.transform.rotation = Quaternion.Slerp(tamagochiHead.transform.rotation, Quaternion.LookRotation(this.transform.forward), .5f);
    }

    private void LateUpdate()
    {
        if (isAware)
        {
            if (!headRotLimit)
            {
                tamaHead.LookAt(head.position);
                tamaHead.rotation = tamaHead.rotation * Quaternion.Euler(Offset);
            }
        }
    }

    public void Aware()
    {
        if (canAware && stats.isAwake)
        {
            StartCoroutine(AwareCycle());
            canAware = false;
        }
    }

    IEnumerator AwareCycle()
    {
        if(angle < 85f)
        {
            isAware = true;
            stats.attention += Random.Range(0, 15);
            yield return new WaitForSeconds(stats.awareDuration);
        }
        isAware = false;
        controller.canNewState = true;
        yield return new WaitForSeconds(.1f);
        canAware = true;
    }

    private void AwareCheck()
    {
        Vector3 targetDir = head.position - this.transform.position;
        angle = Vector3.Angle(targetDir, this.transform.forward);
        //if(angle > 90)
        //    tamagochiHead.transform.rotation = Quaternion.Slerp(tamagochiHead.transform.rotation, Quaternion.LookRotation(this.transform.forward), .5f);
        if (angle > 70)
            headRotLimit = true;        
        else
            headRotLimit = false;

    }
}
