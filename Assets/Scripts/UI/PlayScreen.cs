using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayScreen : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI currentScoreText;
    private void OnEnable()
    {
        HUDManager.OnPlayEvent += OnHUDEvent;
    }
    private void OnDisable()
    {
        HUDManager.OnPlayEvent -= OnHUDEvent;
    }

    private void OnHUDEvent(ScoreData scoreData)
    {
        currentScoreText.text = String.Format("{0}", scoreData.score);
    }
}
