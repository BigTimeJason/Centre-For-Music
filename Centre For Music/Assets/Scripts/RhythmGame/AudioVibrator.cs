using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVibrator : MonoBehaviour
{
    public GameObject[] spinnies;
    float spinSpeed = -160f;

    void Update()
    {
        float sinWave = Mathf.Sin(Time.time * 3f);
        transform.position = new Vector3(0, 3 + sinWave, 0);

        foreach(GameObject spinny in spinnies)
        {
            spinny.transform.eulerAngles = new Vector3(0, 0, Time.time * spinSpeed);
        }
    }
}
