using System.Collections;
using System.Collections.Generic;
using UnityEngine.Animations;
using UnityEngine;

public class AI_EmotionState : MonoBehaviour {

    //Emotion States - Default is usually neutral/idle
    public enum Emotion {Default, Tired, Asleep, Happy, Sad, Angry, Upset, Wondering};

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
                break;

            case Emotion.Sad:
                //Play Sad Animation
                animator.runtimeAnimatorController = animationController[1];
                break;

            case Emotion.Tired:
                //Play Tired Animation
                animator.runtimeAnimatorController = animationController[2];
                break;

            case Emotion.Asleep:
                //Play Asleep Animation
                animator.runtimeAnimatorController = animationController[3];
                break;

            case Emotion.Angry:
                //Play Angry Animation
                animator.runtimeAnimatorController = animationController[4];
                break;

            case Emotion.Upset:
                //Play Upset Animation
                animator.runtimeAnimatorController = animationController[5];
                break;

            case Emotion.Wondering:
                //Play Confused Animation
                animator.runtimeAnimatorController = animationController[6];
                break;

            default:
                //Play Default Animation
                animator.runtimeAnimatorController = animationController[7];
                break;
        }
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
