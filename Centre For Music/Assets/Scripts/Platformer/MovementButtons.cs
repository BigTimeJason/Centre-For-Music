using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementButtons : MonoBehaviour
{
    private CharacterMovement2D characterMovement;
    public bool isRight;
    private float direction;
    [SerializeField]
    [Range(0.2f, 1f)]
    private float doubleTapTiming = 0.5f;
    private float jumpTimer;

    private void Update()
    {
        if(jumpTimer >= 0)
        {
            jumpTimer -= Time.deltaTime;
        }
    }

    private void Start()
    {
        characterMovement = FindObjectOfType<CharacterMovement2D>();
        direction = isRight == true ? 1 : -1;
    }

    private void OnMouseDown()
    {
        if (jumpTimer >= 0 && !FindObjectOfType<PauseManager>().IsPaused)
        {
            characterMovement.Jump = true;
        }
    }

    private void OnMouseDrag()
    {
        if (!FindObjectOfType<PauseManager>().IsPaused)
        {
            characterMovement.Horizontal = direction;
            jumpTimer = doubleTapTiming;
        }
    }

    private void OnMouseUp()
    {
        characterMovement.Horizontal = 0f;
    }
}
