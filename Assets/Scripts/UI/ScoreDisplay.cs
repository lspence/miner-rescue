using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    Text score;
    GameManger gameManager;

    private void Awake()
    {
        score = GetComponent<Text>();
        gameManager = FindObjectOfType<GameManger>();
    }
    
    private void Update()
    {
        score.text = gameManager.GetScore();
    }
}
