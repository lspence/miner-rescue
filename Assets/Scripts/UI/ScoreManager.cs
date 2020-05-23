using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance = null;

    private int score;

    private Text scoreText;

    private void Awake()
    {
        MakeInstance();

        scoreText = GameObject.Find("Score").GetComponent<Text>();
    }

    private void Start()
    {
        scoreText.text = score.ToString();
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

        if (!LifeManager.isReplay)
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void IncreaseScore(int scoreAmount)
    {
        score += scoreAmount;
        scoreText.text = score.ToString();
    }
}
