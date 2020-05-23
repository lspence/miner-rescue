using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private AudioClip playerDeathSFX;

    //private int lives = 3;
    private int direction = 1;
    private float playDeathDelay = 0.8f;

    private Text livesText;
    private Vector3 startingPosition;
    private Animator anim;
    private AudioSource audio;

    private void Awake()
    {
        livesText = GameObject.Find("Lives").GetComponent<Text>();
        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
    }
    private void Start()
    {
        //livesText.text = lives.ToString(); 
        startingPosition = GetComponent<Transform>().position;
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == Tags.BAT || target.tag == Tags.RAT || target.tag == Tags.SNAKE || target.tag == Tags.VENOM || target.tag == Tags.SPIDER)
        {
            LooseLife();
        }
    }

    private void LooseLife()
    {
        //lives--;

        //if (lives == 0)
        //{
        //    GameManger.instance.LoadGameOver();
        //}

        LifeManager.instance.UpdateLives();

        audio.PlayOneShot(playerDeathSFX, 0.6f);
        anim.Play("Death");
        //livesText.text = lives.ToString();

        StartCoroutine(PlayerDied(playDeathDelay));
    }

    IEnumerator PlayerDied(float timer)
    {
        yield return new WaitForSeconds(timer);
        gameObject.transform.position = startingPosition;
        anim.Play("Idle");

        Vector3 tempScale = transform.localScale;
        tempScale.x = direction;
        transform.localScale = tempScale;
    }
}
