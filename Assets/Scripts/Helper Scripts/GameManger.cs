using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManger : MonoBehaviour
{
    public static GameManger instance;

    private int score;
    private int lives = 3;

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
        else if (instance != null)
        {
            Destroy(gameObject);
        }
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
        StartCoroutine(PlayerDied(0.8f));
        StartCoroutine(Respawn());

        if (lives == 0)
        {
            Debug.Log("Game Over");
        }
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
        yield return new WaitForSeconds(2f);
        player.transform.position = startingPosition;
        player.SetActive(true);
    }
}
