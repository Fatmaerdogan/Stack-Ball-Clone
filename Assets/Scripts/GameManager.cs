using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using Color = UnityEngine.Color;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool _isOverPowered, _isClicked;
    int score = 0;
    int level = 0;
    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            score += value;
        }
    }
    public int Level
    {
        get
        {
            level = PlayerPrefs.GetInt("Level",1);
            return level;
        }
        set
        {
            level += value;
            PlayerPrefs.SetInt("Level", level);
        }
    }
    private void Awake()
    {
        Instance = this;
    }
    void ScoreSet(int value)
    {
        score = value;
        if (score > PlayerPrefs.GetInt("Highscore", 0))
        {
            PlayerPrefs.SetInt("Highscore", score);
        }
        Events.ScoreTextWrite?.Invoke(score);
    }
    private void Start()
    {
        Events.Score += ScoreSet;
    }
    private void OnDestroy()
    {
        Events.Score -= ScoreSet;
    }
}
