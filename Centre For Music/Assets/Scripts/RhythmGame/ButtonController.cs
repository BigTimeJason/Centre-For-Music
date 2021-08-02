using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    private SpriteRenderer theSR;
    public Sprite defaultImage;
    public Sprite pressedImage;

    // Start is called before the first frame update
    void Start()
    {
        theSR = GetComponent<SpriteRenderer>();
        
    }

    private void OnMouseDrag()
    {
        theSR.sprite = pressedImage;
    }

    private void OnMouseUp()
    {
        theSR.sprite = defaultImage;
    }
}
