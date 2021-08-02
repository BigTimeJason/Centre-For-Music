using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NextLevelButton : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    public AudioClip clip;

    private void OnMouseDown()
    {
        AudioSource.PlayClipAtPoint(clip, new Vector3(0, 0, -10));
        Puzzle puzzleManager = FindObjectOfType<Puzzle>();
        if (puzzleManager.level == 3)
        {
            FindObjectOfType<LevelLoader>().LoadLevel(true);
            return;
        }
        puzzleManager.NextLevel();
        gameObject.SetActive(false);
    }

    public void Show(string text)
    {
        textMeshPro.SetText(text);
    }
}
