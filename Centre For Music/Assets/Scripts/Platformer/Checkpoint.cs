using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Checkpoint : MonoBehaviour
{
    public CanvasGroup text;
    public ParticleSystem particles;
    Vector3 currVelocity;
    bool hasBeenHit;

    public void HitCheckpoint()
    {
        if (!hasBeenHit)
        {
            hasBeenHit = true;
            particles.Play();
            text.alpha = 1;
            StartCoroutine(FloatText());
        }
    }

    IEnumerator FloatText()
    {
        while (text.alpha > 0)
        {
            text.alpha -= Time.deltaTime;
            yield return null;
        }
    }
}
