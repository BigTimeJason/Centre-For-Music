using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLevel : MonoBehaviour
{
    [SerializeField]
    private string gameName;
    public float timeToComplete = 180f;

    private void Update()
    {
        if (timeToComplete > 0)
        {
            timeToComplete -= 1f * Time.deltaTime;
        } else { timeToComplete = 0; }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log(FindObjectOfType<PlayerInformation>().isGameDone(gameName));
            FindObjectOfType<PlayerInformation>().setGameDone(gameName, true);
            FindObjectOfType<PlayerInformation>().addGamePoints(gameName, (int)timeToComplete);
            Debug.Log(FindObjectOfType<PlayerInformation>().isGameDone(gameName));
        }
    }
}
