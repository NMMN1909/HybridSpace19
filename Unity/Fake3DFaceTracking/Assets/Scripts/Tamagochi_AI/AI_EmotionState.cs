using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_EmotionState : MonoBehaviour {

    //Emotion States - Default is usually neutral/idle
    public enum Emotion {Default, Tired, Asleep, Happy, Sad, Angry, Upset, Wondering};

    //Initialze
    public Emotion emotion;

    private void Face()
    {
        switch (emotion)
        {
            case Emotion.Happy:
                //Play Happy Animation
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
}
