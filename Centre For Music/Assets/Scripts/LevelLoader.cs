using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;

    public void LoadLevel(string name)
    {
        FindObjectOfType<AFKManager>().resetTimer();
        StartCoroutine(AnimateLoadLevel(name));
    }

    public void LoadLevel(bool setGameDone = false)
    {
        FindObjectOfType<AFKManager>().resetTimer();
        FindObjectOfType<PlayerInformation>().setGameDone(SceneManager.GetActiveScene().name, setGameDone);
        StartCoroutine(AnimateLoadLevel("MainScene"));
    }

    public void LoadLevel()
    {
        FindObjectOfType<AFKManager>().resetTimer();
        StartCoroutine(AnimateLoadLevel("MainScene"));
    }

    IEnumerator AnimateLoadLevel(string name)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(name);
    }
}
