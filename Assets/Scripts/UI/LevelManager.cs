using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance = null;

    private Text levelText;

    private void Awake()
    {
        levelText = GameObject.Find("Level").GetComponent<Text>();
    }
    private void Start()
    {
        int level = SceneManager.GetActiveScene().buildIndex + 1;
        levelText.text = level.ToString();
    }
}
