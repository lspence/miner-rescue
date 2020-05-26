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
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        //DontDestroyOnLoad(gameObject);
    }

    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }


    //public void IncreaseScore(int scoreAmount)
    //{
    //    score += scoreAmount;
    //    scoreText.text = score.ToString();
    //}
}
