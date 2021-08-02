using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField]
    private bool isPaused = false;

    public bool IsPaused { get => isPaused; set => isPaused = value; }


}
