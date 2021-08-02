using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drum : MonoBehaviour
{
    public AudioClip clip;
    public Sprite buttonDown;
    public Sprite buttonUp;

    private void OnMouseDown()
    {
        GetComponent<SpriteRenderer>().sprite = buttonDown;
        AudioSource.PlayClipAtPoint(clip, new Vector3(0,0,-10));
    }
    private void OnMouseUp()
    {
        GetComponent<SpriteRenderer>().sprite = buttonUp;
    }
}
