using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameWinPanel : Panel
{
    public Text finishLevelText;
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
        if (finish)
        {
            base.PanelActivityStatus(true);
            GameManager.Instance.Level = 1;
            finishLevelText.text = "Level " + GameManager.Instance.Level;
        }

    }
}
