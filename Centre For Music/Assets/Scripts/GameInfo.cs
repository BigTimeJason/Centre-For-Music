using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameInfo
{
    public GameInfo(string name, int currScore, int highScore, bool isDone)
    {
        Name = name;
        CurrScore = currScore;
        HighScore = highScore;
        IsDone = isDone; 
    }
    public string Name { get; set; }
    public int CurrScore { get; set; }
    public int HighScore { get; set; }
    public bool IsDone { get; set; }
}
