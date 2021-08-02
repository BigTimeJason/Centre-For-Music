using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInformation : MonoBehaviour
{
    public bool hasEntered;
    public List<GameInfo> gameInfo = new List<GameInfo>();

    [SerializeField]
    private static PlayerInformation _instance;

    public void Start()
    {
        GameInfo puzzleGame = new GameInfo("Puzzle", 0, 0, false);
        GameInfo quizGame = new GameInfo("Quiz", 0, 0, false);
        GameInfo platformGame = new GameInfo("Platform", 0, 0, false);
        GameInfo rhythmGame = new GameInfo("Rhythm", 0, 0, false);

        gameInfo.Add(puzzleGame);
        gameInfo.Add(quizGame);
        gameInfo.Add(platformGame);
        gameInfo.Add(rhythmGame);
    }

    public void addGamePoints(string game, int points)
    {
        int index = gameInfo.FindIndex(x => x.Name == game);
        if (index >= 0)
        {
            gameInfo[index].CurrScore += points;
            if(gameInfo[index].CurrScore > gameInfo[index].HighScore)
            {
                gameInfo[index].HighScore = gameInfo[index].CurrScore;
            }
        }
    }

    public bool isGameDone(string game)
    {
        int index = gameInfo.FindIndex(x => x.Name == game);
        if (index >= 0)
        {
            return gameInfo[index].IsDone;
        }
        return false;
    }

    public void setGameDone(string game, bool done)
    {
        int index = gameInfo.FindIndex(x => x.Name == game);
        if (index >= 0)
        {
            gameInfo[index].IsDone = done;
        }
    }

    public static PlayerInformation Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<PlayerInformation>();
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
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}