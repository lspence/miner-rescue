using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManger : MonoBehaviour
{
    public static GameManger instance = null;

    private int score;
    private int lives = 3;
    private float playDeathDelay = 0.8f;
    private float respawnDelay = 2f;
    private float levelLoadDelay = 3f;

    private Text lifeText;
    private Text scoreText;
    private Text timeText;
    private GameObject player;
    private Vector3 startingPosition;

    [HideInInspector]
    public bool isPlayerAlive;

    public float timerTime = 99f;

    private void Awake()
    {
        MakeInstance();

        lifeText = GameObject.Find("Lives").GetComponent<Text>();
        scoreText = GameObject.Find("Score").GetComponent<Text>();
        timeText = GameObject.Find("Time").GetComponent<Text>();
        player = GameObject.FindGameObjectWithTag(Tags.PLAYER);
    }
    private void Start()
    {
        startingPosition = player.GetComponent<Transform>().position;
        isPlayerAlive = true;
        lifeText.text = lives.ToString();
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

    public void LooseLife()
    {
        lives--;

        if (lives < 0)
        {
            lives = 0;
        }

        player.GetComponent<Animator>().Play("Death");
        lifeText.text = lives.ToString();
        StartCoroutine(PlayerDied(playDeathDelay));
        StartCoroutine(Respawn());

        if (lives == 0)
        {
            Debug.Log("Game Over");
        }
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
            Debug.Log("Game Over");
        }
    }

    IEnumerator PlayerDied(float timer)
    {
        yield return new WaitForSeconds(timer);
        player.SetActive(false);
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(respawnDelay);
        player.transform.position = startingPosition;
        player.SetActive(true);
    }

    IEnumerator LoadLevel(int level)
    {
        yield return new WaitForSecondsRealtime(levelLoadDelay);
        timerTime = 99f;
        SceneManager.LoadScene(level);
    }
}
