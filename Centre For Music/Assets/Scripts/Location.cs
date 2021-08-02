using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider2D))]
public class Location : MonoBehaviour
{
    public AudioClip clicked, entered;
    public string locationName;
    [SerializeField]
    private bool canClick = false;
    private float timesClicked = 0;
    private LevelLoader levelLoader;
    [TextArea(10, 10)]
    public string description;
    [TextArea(10, 10)]
    public string finishedGameDescription;

    private void Start()
    {
        levelLoader = FindObjectOfType<LevelLoader>();
    }

    private void OnMouseExit()
    {
        transform.localScale = new Vector3(1, 1, 1);
        timesClicked = 0;
    }
    private void OnMouseEnter()
    {
        transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
    }

    private void OnMouseDown()
    {
        if (PlayerInformation.Instance.isGameDone(locationName))
        {
            FindObjectOfType<TextManager>().write(finishedGameDescription);
        } else
        {
            FindObjectOfType<TextManager>().write(description);
        }
        if (canClick)
        {
            if (timesClicked > 0)
            {
                AudioManager.Instance.PlayOneShot("DoorEnter");
                levelLoader.LoadLevel(locationName);
            } else
            {
                AudioManager.Instance.PlayOneShot("DoorClick");
            }
        }
        timesClicked++;
    }

    public void setCanClick(bool click)
    {
        canClick = click;
        gameObject.SetActive(click);
    }
}
