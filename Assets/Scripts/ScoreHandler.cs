using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI bestTimeText;
    [SerializeField] private TextMeshProUGUI dodgeMissileText;
    [SerializeField] private TextMeshProUGUI missileBonusText;




    private int finalTime;
    public static int score;
    public static int missileCount;
    public static int missileBonus;
    float timer = 0.0f;
    public bool isGameOver;
    private int bestTime;
    private float startTime;

    void Start()
    {
        startTime = Time.time;
        isGameOver = false;
        bestTime = PlayerPrefs.GetInt(nameof(bestTime));
        BestTimeMethod(bestTime);
    }

    void Update()
    {
        var currentTime = Mathf.FloorToInt(Time.time - startTime);
        if (currentTime > bestTime)
        {
            // Make sure to also update the local field
            bestTime = currentTime;
            print("best time: " + bestTime);
            PlayerPrefs.SetInt(nameof(bestTime), currentTime);
        }
        if (!isGameOver)
        {
            Timer();
        }
        else if(isGameOver)
        {
            bestTime = PlayerPrefs.GetInt(nameof(bestTime));
            BestTimeMethod(bestTime);
        }

        scoreText.text = ""+score;
        dodgeMissileText.text =""+ missileCount;
        if(missileBonus!=0)
        {
            missileBonusText.text = "+" + missileBonus;
        }
       
    }

    void Timer()
    {
        timer += Time.deltaTime;
        float seconds = Mathf.FloorToInt(timer % 60);
        float min = Mathf.FloorToInt(timer / 60);
        timerText.text = string.Format("{0:00}:{1:00}", min, seconds);
    }

    public void HighScoreMethod()
    {
        if (score > PlayerPrefs.GetFloat("HighScore", 0f))
        {
            print("High score: " + score);
            PlayerPrefs.SetFloat("HighScore", score);
            // bestScoreText.text = "High Score: " + PlayerPrefs.GetFloat("HighScore");
        }
        else
        {
            // bestScoreText.text = "High Score: " + PlayerPrefs.GetFloat("HighScore");
        }
    }

    public void BestTimeMethod(int bestTime)
    {
        float seconds = Mathf.FloorToInt(bestTime % 60);
        float min = Mathf.FloorToInt(bestTime / 60);
        bestTimeText.text = string.Format("{0:00}:{1:00}", min, seconds);
    }
}
