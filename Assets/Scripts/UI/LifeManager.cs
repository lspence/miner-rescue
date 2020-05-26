using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour
{
    public static LifeManager instance = null;

    private int lives = 3;
    //private bool isAlive;

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

    //private void Update()
    //{
    //    if (!isAlive)
    //    {
    //        GameManger.instance.LoadGameOver();
    //    }
    //}

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

        //if (instance == null)
        //{
        //    instance = this;

        //}
        //else if (instance != this)
        //{
        //    Destroy(gameObject);
        //}

        //DontDestroyOnLoad(gameObject);
        //if (!GameManger.isReplay)
        //{
        //    DontDestroyOnLoad(gameObject);
        //}
        //else
        //{
        //    lives = 3;
        //}
    }

    
    public void UpdateLives()
    {
        lives--;

        //if (lives == 0)
        //{
        //    isAlive = false;
        //    //GameManger.instance.LoadGameOver();
        //}

        livesText.text = lives.ToString();
    }

    public string GetLives()
    {
        return lives.ToString();
    }
}
