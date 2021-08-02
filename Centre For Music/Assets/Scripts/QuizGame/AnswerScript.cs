using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerScript : MonoBehaviour
{
    public AudioClip clip, incorrectAudio;
    public bool isCorrect = false;
    private QuizManager quizManager;

    private void Start()
    {
        quizManager = FindObjectOfType<QuizManager>();
    }
    public void Answer()
    {
        if (isCorrect)
        {
            AudioSource.PlayClipAtPoint(clip, new Vector3(0, 0, -10));
        } else
        {
            //AudioSource.PlayClipAtPoint(incorrectAudio, new Vector3(0, 0, -10));
        }
        quizManager.AnswerQuestion(isCorrect);
    }
}
