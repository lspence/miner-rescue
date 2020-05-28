using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private AudioClip playerDeathSFX;

    private int direction = 1;
    private float playDeathDelay = 0.8f;

    private Vector3 startingPosition;
    private Animator anim;
    private AudioSource audio;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
    }
    private void Start()
    {
        startingPosition = GetComponent<Transform>().position;
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == Tags.BAT || target.tag == Tags.RAT || target.tag == Tags.SNAKE || target.tag == Tags.VENOM || target.tag == Tags.SPIDER || target.tag == Tags.OBSTACLE)
        {
            LooseLife();
        }
    }

    private void LooseLife()
    {
        GameManger.instance.UpdateLives();

        audio.PlayOneShot(playerDeathSFX, 0.6f);
        anim.Play("Death");

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
