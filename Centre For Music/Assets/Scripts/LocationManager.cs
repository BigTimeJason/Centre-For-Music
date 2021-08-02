using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationManager : MonoBehaviour
{
    public Animator transitionAnimator;
    public Animator titleAnimator;

    public Location[] locations;
    public GameObject[] splashScreenItems;

    void Start()
    {
        locations = FindObjectsOfType<Location>();
        ToggleSplashScreen();
    }

    public void ToggleSplashScreen()
    {
        foreach (GameObject gameObject in splashScreenItems)
        {
            gameObject.SetActive(!PlayerInformation.Instance.hasEntered);
        }

        foreach (Location location in locations)
        {
            if(location.locationName != "Rhythm") location.setCanClick(PlayerInformation.Instance.hasEntered);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && !PlayerInformation.Instance.hasEntered)
        {
            AudioManager.Instance.PlayOneShot("DoorEnter");
            PlayerInformation.Instance.hasEntered = true;
            transitionAnimator.Play("Transition", 0);
            titleAnimator.Play("Slide Out", 0);
        }
    }
}
