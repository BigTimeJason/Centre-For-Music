using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Vector3 previousPosition;
    public Vector3 changePosition;

    public int sceneNumber;
    [SerializeField] private bool isMoving;
    public float ratio, duration, movingTimer, movingDuration;

    private void Start()
    {
        previousPosition = transform.position;
    }

    private void Update()
    {
        movingTimer += Time.deltaTime;
        if(movingTimer >= movingDuration)
        {
            movingTimer = 0;
            startMoving(Random.value > 0.5f);
        }
    }

    private void OnMouseDown()
    {
        if(FindObjectOfType<LocationScroller>().sceneNumber == -1) Application.OpenURL("https://www.uwe.ac.uk/life/activities/centre-for-music");
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
        }
        else if (!isRight && sceneNumber != -1)
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
            changePosition = new Vector3(5.5f, 0, 0);
        }
        else
        {
            changePosition = new Vector3(-5.5f, 0, 0);
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
