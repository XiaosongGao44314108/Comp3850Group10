using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int[] scores;
    public float[] times;
    public int[] attempts;
    public bool[] levelsLockstates;

    public PlayerData(GameManager gameManager)
    {
        scores = gameManager.Scores;
        times = gameManager.Times;
        attempts = gameManager.Attempts;
        levelsLockstates = gameManager.LevelsLockstates;
        correctAnswers = gameManager.CorrectAnswers;
        wrongAnswers = gameManager.WrongAnswers;
        optFeedback = gameManager.OptFeedback;
        videoFeedback = gameManager.VideoFeedback;
        textFeedback = gameManager.TextFeedback;
        goodFeedback = gameManager.GoodFeedback;
        badFeedback = gameManager.BadFeedback;
    }
}
