using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : Panel
{
    public Text  gameOverScoreText, gameOverBestText;
    private void Start()
    {
        Events.GameFinish += GameFinish;
    }
    private void OnDestroy()
    {
        Events.GameFinish -= GameFinish;
    }
    void GameFinish(bool finish)
    {
        if (!finish)
        {
            base.PanelActivityStatus(true);
            gameOverScoreText.text = "SCORE: \n" + GameManager.Instance.Score.ToString();
            gameOverBestText.text = "BEST SCORE: \n" + PlayerPrefs.GetInt("Highscore").ToString();
        }

    }
}
