using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReplayButton : MonoBehaviour
{
    public void ReplayGame()
    {
        Time.timeScale = 1f;
        LifeManager.isReplay = true;
        SceneManager.LoadScene("Level1");
    }
}
