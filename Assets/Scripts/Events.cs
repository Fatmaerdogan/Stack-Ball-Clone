using System;
using UnityEngine;

public class Events 
{
    public static Action<bool> GameFinish;
    public static Action<bool> FireEffect;
    public static Action<float> SliderFill;
    public static Action OverPowerFill;
    public static Action<int> Score;
    public static Action<Color> Color;
    public static Action<int> ScoreTextWrite;

}
public enum PlayerState
{
    Prepare,
    Play,
    Dead,
    Finish
}
