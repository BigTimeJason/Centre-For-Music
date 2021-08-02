using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyButton : MonoBehaviour
{
    public AudioClip clip;
    public void SetDifficulty(int diff)
    {
        AudioSource.PlayClipAtPoint(clip, new Vector3(0, 0, -10));
        FindObjectOfType<QuizManager>().StartQuiz(diff);
    }
}
