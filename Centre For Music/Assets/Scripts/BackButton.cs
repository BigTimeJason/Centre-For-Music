using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class BackButton : MonoBehaviour
{
    private LevelLoader levelLoader;
    private void Start()
    {
        levelLoader = FindObjectOfType<LevelLoader>();
    }

    private void OnMouseDown()
    {
        levelLoader.LoadLevel();
    }
}
