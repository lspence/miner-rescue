using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RescuedScript : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == Tags.PLAYER)
        {
            anim.Play("Rescued");
            GameManger.instance.LoadNextLevel(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
