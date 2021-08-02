using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationScrollerButton : MonoBehaviour
{
    public bool isRight;

    private void OnMouseDown()
    {
        if (isRight)
        {
            AudioManager.Instance.PlayOneShot("RightArrow");
        } else
        {
            AudioManager.Instance.PlayOneShot("LeftArrow");
        }
        FindObjectOfType<LocationScroller>().startMoving(isRight);
        FindObjectOfType<AFKManager>().resetTimer();
    }
}
