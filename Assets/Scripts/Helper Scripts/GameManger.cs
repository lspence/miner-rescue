using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManger : MonoBehaviour
{
    public static GameManger instance = null;

    [SerializeField] private AudioClip playerDeathSFX;

    private int score;
    private int lives;
    private float timerTime;
    private float levelLoadDelay = 3f;
    private float gameoverLoadDelay = 1f;

    private Text lifeText;
    private Text levelText;
    private Text scoreText;
    private Text timeText;

    private void Awake()
    {
        MakeInstance();

        lifeText = GameObject.Find("Lives").GetComponent<Text>();
        levelText = GameObject.Find("Level").GetComponent<Text>();
        scoreText = GameObject.Find("Score").GetComponent<Text>();
        timeText = GameObject.Find("Time").GetComponent<Text>();
    }
    private void Start()
    {
        lifeText.text = lives.ToString();
        int level = SceneManager.GetActiveScene().buildIndex + 1;
        levelText.text = level.ToString();
        timerTime = 99f;
        scoreText.text = score.ToString();
    }
    
    private void Update()
    {
        RemainingTime();
    }

    private void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void IncreaseScore(int scoreAmount)
    {
        score += scoreAmount;
        scoreText.text = score.ToString();
    }

   
    public void LoadNextLevel(int level)
    {
        StartCoroutine(LoadLevel(level));
    }

    private void RemainingTime()
    {
        timerTime -= Time.deltaTime;
        timeText.text = timerTime.ToString("F0");

        if (timerTime <= 1)
        {
            StartCoroutine(LoadDelay());
        }
    }
    

    IEnumerator LoadLevel(int nextLevel)
    {
        yield return new WaitForSecondsRealtime(levelLoadDelay);
        timerTime = 100f;
        SceneManager.LoadScene(nextLevel);
    }

    public void LoadGameOver()
    {
        
        StartCoroutine(LoadDelay());
    }

    IEnumerator LoadDelay()
    {
        timerTime = 100f;
        yield return new WaitForSecondsRealtime(gameoverLoadDelay);
        SceneManager.LoadScene("GameOver");
    }
}
