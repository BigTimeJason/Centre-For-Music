using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextManager : MonoBehaviour
{
    public AudioClip clip;
    public TextMeshProUGUI description;
    public int index;
    public float timeBetweenCharacters;
    private Animator animator;
    private string currentText;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void write(string text)
    {
        if(text != currentText)
        {
            AudioSource.PlayClipAtPoint(clip, new Vector3(0, 0, -10));
            animator.Play("Base Layer.TextSlideIn", 0, 0);
            clearText();
            currentText = text;
            StartCoroutine(displayText(text));
        }

    }

    public void hide()
    {
        animator.Play("Base Layer.TextSlideOut", 0, 0);
    }

    public void clearText()
    {
        currentText = "";
        index = 0;
        StopAllCoroutines();
    }

    IEnumerator displayText(string text)
    {
        while (description.text != text)
        {
            description.text = text.Substring(0, index);
            yield return new WaitForSeconds(timeBetweenCharacters);
            index++;
        }
        index = 0;
    }
}
