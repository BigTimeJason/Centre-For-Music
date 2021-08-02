using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement2D : MonoBehaviour
{
    public CharacterController2D characterController;

    private float horizontal;
    private bool jump = false;
    public float movespeed = 10;

    public float Horizontal { get => horizontal; set => horizontal = value; }
    public bool Jump { get => jump; set => jump = value; }



    void FixedUpdate()
    {
        characterController.Move(horizontal * movespeed * Time.fixedDeltaTime, false, Jump);
        Jump = false;
    }
}
