using System.Collections;
using System.Collections.Generic;
using UnityEngine.Animations;
using UnityEngine;

public class AI_EmotionState : MonoBehaviour {

    //Emotion States - Default is usually neutral/idle
    public enum Emotion {Default, Tired, Asleep, Happy, Sad, Angry, Upset, Wondering};

    //Reference
    public AI_ThinkingCloud cloud;
    public GameObject thinkingCloudObj;
    private AI_Variables stats;
    private AI_Sleep sleep;

    //Initialze
    public Emotion emotion;
    public Animator animator;
    public RuntimeAnimatorController[] animationController;

    void Update()
    {
        switch (emotion)
        {
            case Emotion.Happy:
                //Play Happy Animation
                animator.runtimeAnimatorController = animationController[0];
                thinkingCloudObj.GetComponent<SpriteRenderer>().sprite = cloud.thinkingCloud[5];
                break;

            case Emotion.Sad:
                //Play Sad Animation
                animator.runtimeAnimatorController = animationController[1];
                thinkingCloudObj.GetComponent<SpriteRenderer>().sprite = cloud.thinkingCloud[6];
                break;

            case Emotion.Tired:
                //Play Tired Animation
                animator.runtimeAnimatorController = animationController[2];
                if(stats.energy > stats.energyToTired && sleep.disturbedBool == false)
                    thinkingCloudObj.GetComponent<SpriteRenderer>().sprite = cloud.thinkingCloud[0];
                else
                    thinkingCloudObj.GetComponent<SpriteRenderer>().sprite = cloud.thinkingCloud[1];
                break;

            case Emotion.Asleep:
                //Play Asleep Animation
                animator.runtimeAnimatorController = animationController[3];
                thinkingCloudObj.GetComponent<SpriteRenderer>().sprite = cloud.thinkingCloud[2];
                break;

            case Emotion.Angry:
                //Play Angry Animation
                animator.runtimeAnimatorController = animationController[4];
                thinkingCloudObj.GetComponent<SpriteRenderer>().sprite = cloud.thinkingCloud[8];
                break;

            case Emotion.Upset:
                //Play Upset Animation
                animator.runtimeAnimatorController = animationController[5];
                thinkingCloudObj.GetComponent<SpriteRenderer>().sprite = cloud.thinkingCloud[8];
                break;

            case Emotion.Wondering:
                //Play Confused Animation
                animator.runtimeAnimatorController = animationController[6];
                thinkingCloudObj.GetComponent<SpriteRenderer>().sprite = cloud.thinkingCloud[3];
                break;

            default:
                //Play Default Animation
                animator.runtimeAnimatorController = animationController[7];
                thinkingCloudObj.GetComponent<SpriteRenderer>().sprite = cloud.thinkingCloud[7];
                break;
        }
    }

    private void Start()
    {
        stats = GetComponent<AI_Variables>();
        sleep = GetComponent<AI_Sleep>();
    }

    /*
    public void Face()
    {
        switch (emotion)
        {
            case Emotion.Happy:
                //Play Happy Animation
                animator.runtimeAnimatorController = animationController[0];
                break;

            case Emotion.Sad:
                //Play Sad Animation
                break;

            case Emotion.Tired:
                //Play Tired Animation
                break;
            case Emotion.Asleep:
                //Play Asleep Animation
                break;

            case Emotion.Angry:
                //Play Angry Animation
                break;

            case Emotion.Upset:
                //Play Upset Animation
                break;

            case Emotion.Wondering:
                //Play Confused Animation
                break;

             default:
                //Play Default Animation
                break;
        }
    }
    */
}
