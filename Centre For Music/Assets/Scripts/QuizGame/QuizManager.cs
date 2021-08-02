using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuizManager : MonoBehaviour
{
    public AudioClip clip;
    public AudioClip correctSound, incorrectSound;
    public List<QuestionsAndAnswers> easyQuestions;
    public List<QuestionsAndAnswers> medQuestions;
    public List<QuestionsAndAnswers> hardQuestions;
    public List<QuestionsAndAnswers> currQuestions;
    public GameObject[] options;
    public GameObject[] difficultyButtons;
    public int currentQuestion;

    public int answeredQuestions;
    public int correctQuestions;

    public TextManager textManager;

    private void Start()
    {
        textManager = FindObjectOfType<TextManager>();
        answeredQuestions = 0;
        correctQuestions = 0;
    }

    private void SetAnswers()
    {
        for (int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<AnswerScript>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = currQuestions[currentQuestion].answers[i];
            if (currQuestions[currentQuestion].correctAnswer == i + 1)
            {
                options[i].GetComponent<AnswerScript>().isCorrect = true;
            }
        }
    }

    private void GenerateQuestion()
    {
        currentQuestion = Random.Range(0, currQuestions.Count);

        textManager.write(currQuestions[currentQuestion].questions);
        SetAnswers();

        currQuestions.RemoveAt(currentQuestion);
    }

    public void AnswerQuestion(bool correct)
    {
        answeredQuestions += 1;
        if (correct)
        {
            correctQuestions += 1;
        }
        else
        {

        }
        if (answeredQuestions < 5)
        {
            GenerateQuestion();
        } else
        {
            EndQuiz();
        }
    }

    private void Update()
    {

    }

    public void StartQuiz(int difficulty)
    {
        Debug.Log("yes");
        switch (difficulty)
        {
            case 1:
                currQuestions = easyQuestions.ConvertAll(questionsAndAnswers => new QuestionsAndAnswers(questionsAndAnswers.questions, questionsAndAnswers.answers, questionsAndAnswers.correctAnswer));
                break;
            case 2:
                currQuestions = medQuestions.ConvertAll(questionsAndAnswers => new QuestionsAndAnswers(questionsAndAnswers.questions, questionsAndAnswers.answers, questionsAndAnswers.correctAnswer));
                break;
            case 3:
                currQuestions = hardQuestions.ConvertAll(questionsAndAnswers => new QuestionsAndAnswers(questionsAndAnswers.questions, questionsAndAnswers.answers, questionsAndAnswers.correctAnswer));
                break;
        }
        for (int i = 0; i < difficultyButtons.Length; i++)
        {
            difficultyButtons[i].SetActive(false);
        }
        for (int i = 0; i < options.Length; i++)
        {
            options[i].SetActive(true);
        }
        GenerateQuestion();
    }

    public void EndQuiz()
    {
        AudioSource.PlayClipAtPoint(clip, new Vector3(0, 0, -10));
        for (int i = 0; i < options.Length; i++)
        {
            options[i].SetActive(false);
        }

        FindObjectOfType<PlayerInformation>().addGamePoints("Quiz", correctQuestions);
        if (correctQuestions > 4)
        {
            textManager.write("You answered " + correctQuestions + " correctly... Fine you can have your singer back. You can safely back out of this game using the back button.");
            FindObjectOfType<PlayerInformation>().setGameDone("Quiz", true);
        } else
        {
            textManager.write("You answered " + correctQuestions + " correctly... You'll need to do better than that to get your singer back.");
        }
    }
}
