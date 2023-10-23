using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GamePanel : Panel
{
    public Image levelSlider, levelSliderFill, currentLevelImage, nextLevelImage, _overpowerFill;
    public Text currentLevelText, nextLevelText,startText,ScoreText;
    public GameObject _overpowerBar;
    private float _overpowerBuildUp;
    private Player _player;
    void Awake()
    {
        _player = FindObjectOfType<Player>();
    }
    private void Start()
    {
        Events.SliderFill += LevelSliderFill;
        Events.OverPowerFill += OverPowerFill;
        Events.ScoreTextWrite += ScoreTextWrite;
        Events.Color += ColorSet;
        currentLevelText.text = GameManager.Instance.Level.ToString();
        nextLevelText.text = GameManager.Instance.Level + 1 + "";
    }
    private void OnDestroy()
    {
        Events.SliderFill -= LevelSliderFill;
        Events.OverPowerFill -= OverPowerFill;
        Events.ScoreTextWrite -= ScoreTextWrite;
        Events.Color -= ColorSet;
    }
    public void ColorSet(Color color)
    {
        levelSlider.color = nextLevelImage.color = currentLevelImage.color = color;
        levelSliderFill.color = color + Color.gray;
    }
    public void LevelSliderFill(float fillAmount)
    {
        levelSlider.fillAmount = fillAmount;
    }
    public void ScoreTextWrite(int value)
    {
        ScoreText.text = value.ToString();
    }
    public void GameStart()
    {
        _player.playerState = PlayerState.Play;
        startText.gameObject.SetActive(false);
    }
    public void OverPowerFill()
    {
        if (GameManager.Instance._isOverPowered)
        {
            _overpowerBuildUp -= Time.deltaTime * .3f;
            Events.FireEffect?.Invoke(true);
        }
        else
        {
            Events.FireEffect?.Invoke(false);
            if (GameManager.Instance._isClicked)
                _overpowerBuildUp += Time.deltaTime * .8f;
            else
                _overpowerBuildUp -= Time.deltaTime * .5f;
        }

        if (_overpowerBuildUp >= 0.3f || _overpowerFill.color == Color.red)
            _overpowerBar.SetActive(true);
        else
            _overpowerBar.SetActive(false);

        if (_overpowerBuildUp >= 1)
        {
            _overpowerBuildUp = 1;
            GameManager.Instance._isOverPowered = true;
            _overpowerFill.color = Color.red;
        }
        else if (_overpowerBuildUp <= 0)
        {
            _overpowerBuildUp = 0;
            GameManager.Instance._isOverPowered = false;
            _overpowerFill.color = Color.white;
        }

        if (_overpowerBar.activeInHierarchy)
            _overpowerFill.fillAmount = _overpowerBuildUp;

    }
}
