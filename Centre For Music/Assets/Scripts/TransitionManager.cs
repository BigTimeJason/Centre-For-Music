using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionManager : MonoBehaviour
{
    public GameObject splashScreen;
    private LocationManager locationManager;

    private void Start()
    {
        locationManager = FindObjectOfType<LocationManager>();
    }

    public void ToggleSplashScreen()
    {
        locationManager.ToggleSplashScreen();
    }
}
