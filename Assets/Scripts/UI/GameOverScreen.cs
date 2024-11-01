using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI currentScoreText;
    [SerializeField]
    private TextMeshProUGUI highScoreText;
    [SerializeField]
    private TextMeshProUGUI gameOverText;
    private void Start()
    {
        gameOverText.DOFade(0f, 0.7f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }
    private void OnEnable()
    {
        HUDManager.OnGameOverEvent += OnHUDEvent;
    }
    private void OnDisable()
    {
        HUDManager.OnGameOverEvent -= OnHUDEvent;
    }

    private void OnHUDEvent(ScoreData scoreData)
    {
        currentScoreText.text = String.Format("Score  \n {0}", scoreData.score);
        highScoreText.text = String.Format("Best Score  \n {0}", scoreData.highScore);
    }
}
