using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using System;

public class MenuScreen : MonoBehaviour
{
    [SerializeField]
    private RectTransform title;
    [SerializeField]
    private TextMeshProUGUI tapToPlay;
    [SerializeField]
    private TextMeshProUGUI diamondCountText;
    [SerializeField]
    private TextMeshPro playScreenBestScoreText;
    // Start is called before the first frame update
    void Start()
    {
        title.DOAnchorPosY(-40f, 1f).SetEase(Ease.OutBounce);
        tapToPlay.DOFade(0f, 0.7f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }
    private void OnEnable()
    {
        HUDManager.OnMenuEvent += OnHUDEvent;
    }
    private void OnDisable()
    {
        HUDManager.OnMenuEvent -= OnHUDEvent;
    }

    private void OnHUDEvent(ScoreData scoreData)
    {
        diamondCountText.text = String.Format(" x {0}", scoreData.diamondCount);
        playScreenBestScoreText.text = String.Format("BEST SCORE : {0}", scoreData.highScore);
    }
}
