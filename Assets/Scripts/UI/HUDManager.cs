using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

[Serializable]
public struct ScoreData
{
    public int score;
    public int highScore;
    public int diamondCount;
}
public class HUDManager : MonoBehaviour
{
    public GameObject menuScreen;
    public GameObject playScreen;
    public GameObject gameOverScreen;

    private CanvasScaler canvasScaler;
    private ScoreData currentScoreData;

    public delegate void HUDEvent(ScoreData scoreData);
    public static event HUDEvent OnMenuEvent;
    public static event HUDEvent OnPlayEvent;
    public static event HUDEvent OnGameOverEvent;

    private void OnEnable()
    {
        GameManager.OnGameEvent += OnGameEvent;
        Diamond.OnDiamondTaken += OnDiamondScore;
    }


    private void OnDisable()
    {
        GameManager.OnGameEvent -= OnGameEvent;
        Diamond.OnDiamondTaken -= OnDiamondScore;
    }
    private void Awake()
    {
        canvasScaler = GetComponent<CanvasScaler>();
        canvasScaler.referenceResolution = new Vector2(Screen.width, Screen.height);

    }
    private void Start()
    {
        Initialise();
    }

    private void OnGameEvent(eGameEvent gameEvent)
    {
        switch (gameEvent)
        {
            case eGameEvent.GAME_OVER:
                ShowGameOver();
                break;

            case eGameEvent.GAME_START:
                ShowGameStart();
                break;
            case eGameEvent.GAME_SCORE:
                UpdateScore(1);
                break;
            case eGameEvent.GAME_RESTART:
                Initialise();
                break;
            default:
                break;
        }
    }

    private void Initialise()
    {
        gameOverScreen.SetActive(false);
        menuScreen.SetActive(true);
        playScreen.SetActive(false);
        //Loading the score Data from the player prefs
        currentScoreData.score = 0;
        currentScoreData.highScore = LoadHighScore();
        currentScoreData.diamondCount = PlayerPrefs.GetInt("Diamonds");
        OnMenuEvent?.Invoke(currentScoreData);
    }

    public void SaveHighScore()
    {
        PlayerPrefs.SetInt("HighScore", currentScoreData.highScore);

    }
    public int LoadHighScore()
    {
        return PlayerPrefs.GetInt("HighScore");
    }
    private void ShowGameStart()
    {
        gameOverScreen.SetActive(false);
        menuScreen.SetActive(false);
        playScreen.SetActive(true);

    }
    public void ShowGameRestart()
    {

        currentScoreData.highScore = LoadHighScore();
        currentScoreData.score = 0;
        UpdateScore(currentScoreData.score);
        gameOverScreen.SetActive(false);
        menuScreen.SetActive(true);
        playScreen.SetActive(false);
        OnMenuEvent?.Invoke(currentScoreData);
    }
    private void ShowGameOver()
    {
        if (currentScoreData.score > currentScoreData.highScore)
        {
            currentScoreData.highScore = currentScoreData.score;
            SaveHighScore();
        }
        PlayerPrefs.SetInt("Diamonds", currentScoreData.diamondCount);
        gameOverScreen.SetActive(true);
        menuScreen.SetActive(false);
        playScreen.SetActive(false);
        OnGameOverEvent?.Invoke(currentScoreData);
    }

    private void UpdateScore(int score)
    {
        currentScoreData.score += score;
        OnPlayEvent?.Invoke(currentScoreData);

    }
    private void OnDiamondScore()
    {
        currentScoreData.diamondCount++;
        UpdateScore(1);
    }


}
