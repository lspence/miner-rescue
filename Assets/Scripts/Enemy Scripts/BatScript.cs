﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatScript : MonoBehaviour
{
    [SerializeField] private AudioClip enemyKilledSFX;

    private int score;
    private int batPoints = 2;
    private float moveSpeed = 2f;
    private float direction = 1.0f;
    private float batPositionOffset = 2f;
    private float batDeathDelay = 0.2f;

    private Vector3 moveDirection = Vector3.right;
    private Vector3 originPosition;
    private Vector3 movePosition;
    private AudioSource audio;

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }


    private void Start()
    {
        originPosition = transform.position;
        originPosition.x += batPositionOffset;

        movePosition = transform.position;
        movePosition.x -= batPositionOffset;
    }

    
    private void Update()
    {
        MoveBat();
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == Tags.BULLET)
        {
            score = batPoints;
            GameManger.instance.IncreaseScore(score);
            audio.PlayOneShot(enemyKilledSFX, 0.6f);
            StartCoroutine(Killed(batDeathDelay));
        }
    }

    private void MoveBat()
    {
        transform.Translate(moveDirection * moveSpeed * Time.smoothDeltaTime);

        if (transform.position.x >= originPosition.x)
        {
            moveDirection = Vector3.left;
            ChangeDirection(-direction);
        }
        else if (transform.position.x <= movePosition.x)
        {
            moveDirection = Vector3.right;
            ChangeDirection(direction);
        }
    }

    private void ChangeDirection(float direction)
    {
        Vector3 tempScale = transform.localScale;
        tempScale.x = direction;
        transform.localScale = tempScale;
    }

    IEnumerator Killed(float timer)
    {
        yield return new WaitForSeconds(timer);
        Destroy(gameObject);
    }
}
