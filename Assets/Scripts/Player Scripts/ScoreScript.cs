using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    //private Text scoreTextScore;

    //private int score;
    private void Start()
    {
        //scoreTextScore = GameObject.Find("Score").GetComponent<Text>();
    }

    
    private void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        //if (target.tag == Tags.BAT)
        //{
        //    score += 2;
        //}

        //scoreTextScore.text = score.ToString();
    }
}
