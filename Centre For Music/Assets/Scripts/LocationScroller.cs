using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationScroller : MonoBehaviour
{
    /// <summary>
    /// This entire script needs a huge clean up it is so bad.
    /// </summary>
    
    public Vector3 previousPosition;
    public Vector3 changePosition;

    public int sceneNumber;
    [SerializeField] private bool isMoving;
    public float ratio, duration;
    public AudioManager audioManager;

    private void Start()
    {
        previousPosition = transform.position;
        audioManager = FindObjectOfType<AudioManager>();

        audioManager.Play("Bass");

        if (PlayerInformation.Instance.isGameDone("Puzzle"))
        {
            audioManager.Play("Drums");
        }
        if (PlayerInformation.Instance.isGameDone("Quiz"))
        {
            audioManager.Play("Pads");
        }
        if (PlayerInformation.Instance.isGameDone("Platform"))
        {
            audioManager.Play("Guitar");
        }
    }

    private void Awake()
    {
        PlayerInformation playerInformation = FindObjectOfType<PlayerInformation>();
        int completedGames = 0;
        for (int i = 0; i < playerInformation.gameInfo.Count; i++)
        {
            if (playerInformation.gameInfo[i].IsDone == true)
            {
                completedGames++;
            }
        }

        if (completedGames >= 3)
        {
            foreach (Location location in FindObjectsOfType<Location>())
            {
                location.setCanClick(true);
            }
        }
    }

    public void startMoving(bool isRight)
    {
        if (isRight && sceneNumber != 1)
        {
            if (!isMoving)
            {
                sceneNumber += 1;
                StartCoroutine(moveInDirection(isRight));
                isMoving = true;
            }
        } else if (!isRight && sceneNumber != -1)
        {
            if (!isMoving)
            {
                sceneNumber -= 1;
                StartCoroutine(moveInDirection(isRight));
                isMoving = true;
            }
        }
    }

    IEnumerator moveInDirection(bool isRight)
    {
        if (!isRight)
        {
            changePosition = new Vector3(18, 0, 0);
        } else
        {
            changePosition = new Vector3(-18, 0, 0);
        }
        ratio = 0f;
        while (ratio < duration)
        {
            ratio += 1 * Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, previousPosition + changePosition, ratio / (duration * 10));
            yield return null;
        }
        previousPosition = transform.position;
        isMoving = false;
    }
}
