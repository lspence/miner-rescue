using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManger : MonoBehaviour
{
    public static GameManger instance = null;

    [SerializeField] private AudioClip playerDeathSFX;

    private int score = 0;
    private int lives = 3;
    private float timerTime;
    private float levelLoadDelay = 1.5f;
    private float gameoverLoadDelay = 0.1f;

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
        int level = SceneManager.GetActiveScene().buildIndex;
        levelText.text = level.ToString();
        timerTime = 60f;
    }
    
    private void Update()
    {
        RemainingTime();
        CheckIfQuitGame();
    }

    private void MakeInstance()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
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

    private void CheckIfQuitGame()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
            ResetGame();
        }
    }

    public void LoadNextLevel(int level)
    {
        StartCoroutine(LoadLevel(level));
    }

    public void DisplayCurrentLevel()
    {
        int level = SceneManager.GetActiveScene().buildIndex + 1;
        levelText.text = level.ToString();
    }

    public string GetScore()
    {
        return score.ToString();
    }

    public void IncreaseScore(int scoreAmount)
    {
        score += scoreAmount;
        scoreText.text = score.ToString();
    }

    public void UpdateLives()
    {
        lives--;

        lifeText.text = lives.ToString();

        if (lives <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    public string GetLives()
    {
        return lives.ToString();
    }

    public void LoadGameOver()
    {

        StartCoroutine(LoadDelay());
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }

    IEnumerator LoadLevel(int nextLevel)
    {
        yield return new WaitForSecondsRealtime(levelLoadDelay);
        timerTime = 60f;
        SceneManager.LoadScene(nextLevel);
        DisplayCurrentLevel();
    }

    IEnumerator LoadDelay()
    {
        timerTime = 60f;
        yield return new WaitForSecondsRealtime(gameoverLoadDelay);
        Time.timeScale = 0f;
        SceneManager.LoadScene("GameOver");
    }
}
