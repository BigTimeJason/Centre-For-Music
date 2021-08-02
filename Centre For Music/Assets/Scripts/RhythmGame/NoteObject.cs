using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public bool canBePressed;

    
    // Start is called before the first frame update
    private void OnMouseDown()
    {
        if (canBePressed)
        {
            FindObjectOfType<ParticleHolder>().playParticle(0, transform.position);

            //GameManager.instance.NoteHit();

            if (Mathf.Abs(transform.position.y) > 0.25)
            {
                Debug.Log("Hit");
                GameManager.instance.NormalHit();
            }
            else if (Mathf.Abs(transform.position.y) > 0.05f)
            {
                Debug.Log("Good");
                GameManager.instance.GoodHit();
            }
            else
            {
                Debug.Log("Perfect");
                GameManager.instance.PerfectHit();
            }

            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Activator"))
        {
            canBePressed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Activator"))
        {
            canBePressed = false;

            if (gameObject.activeInHierarchy) 
            {
                GameManager.instance.NoteMissed();
            }

               
        }
    }
}
