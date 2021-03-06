using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{
    public AudioSource theMusic;
    public AudioClip victory;
    public bool isDone = false;
    public GameObject info;

    public bool startPlaying;

    public BeatScroller theBS;

    public static GameManager instance;

    public int currentScore;
    public int scorePerNote = 100;
    public int scorePerGoodNote = 125;
    public int scorePerPerfectNote = 150;

    public int currentMultiplier;
    public int multiplierTracker;
    public int[] multiplierThresholds;

    public float totalNotes;
    public float normalHits;
    public float goodHits;
    public float perfectHits;
    public float missedHits;

    //public Text scoreText;
    //public Text multiText;

    //public GameObject resultsScreen;
    //public Text percentHitText, normalsText, goodsText, perfectsText, missesText, rankText, finalScoreText; 

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        currentMultiplier = 1;

        totalNotes = FindObjectsOfType<NoteObject>().Length;

    }

    // Update is called once per frame
    void Update()
    {
        if(!startPlaying)
        {
            if(!FindObjectOfType<PauseManager>().IsPaused)
            {
                isDone = false;
                startPlaying = true;
                theBS.hasStarted = true;

                theMusic.Play();
            }
        } else
        {
            if (!theMusic.isPlaying &&  !isDone && !FindObjectOfType<PauseManager>().IsPaused)
            {
                info.GetComponentInChildren<TextMeshProUGUI>().SetText("Congratulations for finishing this game! Hopefully you learned something about the Centre For Music");
                info.SetActive(true);
                isDone = true;
                AudioSource.PlayClipAtPoint(victory, new Vector3(0, 0, -10));
            }
            //if(!theMusic.isPlaying && !resultsScreen.activeInHierarchy)
            //{
            //    resultsScreen.SetActive(true);

            //    normalsText.text = "" + normalHits;
            //    goodsText.text = goodHits.ToString();
            //    perfectsText.text = perfectHits.ToString();
            //    missesText.text = "" + missedHits;

            //    float totalHit = normalHits + goodHits + perfectHits;
            //    float percentHit = (totalHit / totalNotes) * 100f;

            //    percentHitText.text = percentHit.ToString("F1") + "%";

            //    string rankVal = "Try Again";

            //    if(percentHit > 40)
            //    {
            //        rankVal = "You Can Play Better";
            //        if(percentHit > 55)
            //        {
            //            rankVal = "Good Game";
            //            if(percentHit > 70)
            //            {
            //                rankVal = "Great Game";
            //                if(percentHit > 85)
            //                {
            //                    rankVal = "Excellent Performance";
            //                    if (percentHit > 95)
            //                    {
            //                        rankVal = "Flawless!! You Are A Music Superstar";
            //                    }
            //                }
            //            }
            //        }
            //    }

            //    rankText.text = rankVal;

            //    finalScoreText.text = currentScore.ToString();
            //}
        }
        
    }

    public void NoteHit()
    {
        Debug.Log("Hit On Time");
        
        if (currentMultiplier - 1 < multiplierThresholds.Length)
        {                    
            multiplierTracker++;

            if (multiplierThresholds[currentMultiplier - 1] <= multiplierTracker)
            {
                multiplierTracker = 0;
                currentMultiplier++;
            }
        }

        //multiText.text = "Multiplier: x" + currentMultiplier;

        //// currentScore += scorePerNote * currentMultiplier;
        //scoreText.text = "Score: " + currentScore;
    }

    public void NormalHit()
    {
        currentScore += scorePerNote * currentMultiplier;
        NoteHit();
        normalHits++;
    }

    public void GoodHit()
    {
        currentScore += scorePerGoodNote * currentMultiplier;
        NoteHit();
        goodHits++;
    }

    public void PerfectHit()
    {
        currentScore += scorePerPerfectNote * currentMultiplier;
        NoteHit();
        perfectHits++;

    }


    public void NoteMissed()
    {
        Debug.Log("Missed Note");

        currentMultiplier = 1;
        multiplierTracker = 0;

        //multiText.text = "Multiplier: x" + currentMultiplier;
        missedHits++;

    }
}
