using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Colorize : MonoBehaviour {

    private AI_Variables stats;
    private AI_StateMachine stateMachine;


    private float materialLerpProgress;
    private Material targetMat0;
    private Material targetMat1;

    public Material[] creatureMaterials;
    public Material playerMaterial;

    // Use this for initialization
    void Start () {
        stats = GetComponent<AI_Variables>();
        stateMachine = GetComponent<AI_StateMachine>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Colorize()
    {
        if (!stats.colorizeInit)
        {
            materialLerpProgress = 0f;

            targetMat0 = playerMaterial;
            targetMat1 = creatureMaterials[Mathf.RoundToInt(Random.Range(0f, creatureMaterials.Length - 1f))];

            while (targetMat1.color.Equals(targetMat0.color))
                targetMat1 = creatureMaterials[Mathf.RoundToInt(Random.Range(0f, creatureMaterials.Length - 1f))];

            stats.attention += 10f;
            stats.colorizeInit = true;
        }

        materialLerpProgress += .01f;
        playerMaterial.Lerp(targetMat0, targetMat1, materialLerpProgress);

        if (materialLerpProgress >= 1f)
        {
            stats.colorizeInit = false;
            stateMachine.State = AI_StateMachine.state.Roaming;
        }
    }
}
