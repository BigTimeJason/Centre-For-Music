using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AFKManager : MonoBehaviour
{
    [SerializeField]
    private float AFKTimer;
    public float TimeUntilAFK;
    private static AFKManager _instance;

    public static AFKManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<AFKManager>();
            }

            return _instance;
        }
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (_instance == null)
        {
            _instance = this;
        } else
        {
            Destroy(this.gameObject);
        }
    }

    public void resetTimer()
    {
        AFKTimer = 0;
    }

    private void Update()
    {
        if (PlayerInformation.Instance.hasEntered)
        {
            AFKTimer += Time.deltaTime;

            if (AFKTimer >= TimeUntilAFK)
            {
                PlayerInformation.Instance.hasEntered = false;
                FindObjectOfType<LevelLoader>().LoadLevel();
            }
        }
    }
}
