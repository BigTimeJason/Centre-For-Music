using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInfo : MonoBehaviour
{
    public Vector3 currCheckpoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Activator"))
        {
            transform.position = currCheckpoint;
        }

        if (collision.CompareTag("Checkpoint"))
        {
            Checkpoint checkpoint = collision.GetComponent<Checkpoint>();
            checkpoint.HitCheckpoint();
            currCheckpoint = collision.transform.position;
        }

    }
}
