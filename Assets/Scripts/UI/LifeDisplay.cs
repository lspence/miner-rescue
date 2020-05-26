using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeDisplay : MonoBehaviour
{
    Text life;
    GameManger gameManager;

    private void Awake()
    {
        life = GetComponent<Text>();
        gameManager = FindObjectOfType<GameManger>();
    }
    
    private void Update()
    {
        life.text = gameManager.GetLives();
    }
}
