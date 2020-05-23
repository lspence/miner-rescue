using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour
{
    public static LifeManager instance = null;

    private int lives = 3;

    private Text livesText;
    private void Awake()
    {
        MakeInstance();

        livesText = GameObject.Find("Lives").GetComponent<Text>();
    }
    private void Start()
    {
        livesText.text = lives.ToString();
    }


    private void Update()
    {

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

    public void UpdateLives()
    {
        lives--;

        if (lives == 0)
        {
            GameManger.instance.LoadGameOver();
        }

        livesText.text = lives.ToString();
    }

}
