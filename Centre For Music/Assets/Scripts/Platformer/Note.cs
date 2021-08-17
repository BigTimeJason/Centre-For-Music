using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Note : MonoBehaviour
{
    public AudioClip[] clip;
    private float initY;
    public bool isGood;
    // Start is called before the first frame update
    void Start()
    {
        initY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(transform.position.x, initY + 0.3f * Mathf.Sin(Time.time));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        AudioSource.PlayClipAtPoint(clip[Random.Range(0, clip.Length)], transform.position + new Vector3(0,0, -10));
        if (isGood)
        {
            PlayerInformation.Instance.addGamePoints("Platform", 10);
        } else
        {
            PlayerInformation.Instance.addGamePoints("Platform", -10);
        }
        Destroy(gameObject);
    }
}
