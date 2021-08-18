using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Scores : MonoBehaviour
{
    public GameObject highScores, button;

    private void OnMouseDown()
    {
        highScores.SetActive(true);
        button.SetActive(true);

        string highScoresText = "These are the current high scores for this session!\n\n";

        foreach(GameInfo game in PlayerInformation.Instance.gameInfo)
        {
            highScoresText += game.Name + ": " + game.HighScore + "\n";
        }

        highScoresText += "\nYou can improve these by getting a better time!";
        highScores.GetComponentInChildren<TextMeshProUGUI>().SetText(highScoresText);
    }
}
