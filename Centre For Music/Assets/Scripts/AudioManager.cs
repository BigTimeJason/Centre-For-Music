using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public GameObject soundPrefab;
    public Dictionary<string, float> soundTimerDictionary = new Dictionary<string, float>();
    private static AudioManager _instance;

    public void Awake()
    {
        // FindObjectOfType<AudioManager>().Play("PlayerRunning");
        //Debug.Log(soundTimerDictionary.ContainsKey("Bass"));
    }
    public static AudioManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<AudioManager>();
            }

            return _instance;
        }
    }

    public void Play(string name)
    {
        Sound sound = Array.Find(sounds, s => s.name == name);

        if (sound == null)
        {
            Debug.LogError("Sound " + name + " Not Found!");
            return;
        }

        AudioSource audioSource = Instantiate(soundPrefab).GetComponent<AudioSource>();
        audioSource.transform.parent = transform;
        audioSource.clip = sound.clip;
        audioSource.volume = sound.volume;
        audioSource.pitch = sound.pitch;
        audioSource.loop = sound.isLoop;

        audioSource.Play();
    }

    public void PlayOneShot(string name)
    {
        Sound sound = Array.Find(sounds, s => s.name == name);

        if (sound == null)
        {
            Debug.LogError("Sound " + name + " Not Found!");
            return;
        }

        AudioSource.PlayClipAtPoint(sound.clip, transform.position);
    }

    public void Stop(string name)
    {
        Sound sound = Array.Find(sounds, s => s.name == name);

        if (sound == null)
        {
            Debug.LogError("Sound " + name + " Not Found!");
            return;
        }
    }
}