using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class StartActivityButton : MonoBehaviour
{
    public AudioClip clip;
    public GameObject[] toggleStart;

    private void OnMouseDown()
    {
        FindObjectOfType<PauseManager>().IsPaused = false;
        AudioSource.PlayClipAtPoint(clip, new Vector3(0, 0, -10));
        foreach (GameObject gameObject in toggleStart)
        {   
            gameObject.SetActive(!gameObject.activeInHierarchy);
        }
    }
}
