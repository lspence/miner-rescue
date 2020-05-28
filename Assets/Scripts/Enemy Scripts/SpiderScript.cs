using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderScript : MonoBehaviour
{
    private string movementCoroutine = "ChangeMovementDirection";

    [SerializeField] private AudioClip enemyKilledSFX;

    private int score;
    private int spiderPoints = 5;
    private float minDistance = 0.09f;
    private float maxDistance = 0.145f;
    private float spiderDeathDelay = 0.5f;

    private Rigidbody2D myBody;

    private Vector3 moveDirection = Vector3.down;

    private AudioSource audio;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        audio = GetComponent<AudioSource>();
    }
    private void Start()
    {
        StartCoroutine(movementCoroutine);
    }

    
    private void Update()
    {
        MoveSpider();
    }

    private void MoveSpider()
    {
        transform.Translate(moveDirection * Time.smoothDeltaTime);
    }
    

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == Tags.BULLET)
        {
            score = spiderPoints;
            GameManger.instance.IncreaseScore(score);
            audio.PlayOneShot(enemyKilledSFX, 0.6f);
            myBody.bodyType = RigidbodyType2D.Dynamic;
            StartCoroutine(SpiderDeath());
            StopCoroutine(movementCoroutine);
        }
    }

    IEnumerator ChangeMovementDirection()
    {
        yield return new WaitForSeconds(Random.Range(minDistance, maxDistance));

        if (moveDirection == Vector3.down)
        {
            moveDirection = Vector3.up;
        }
        else
        {
            moveDirection = Vector3.down;
        }

        StartCoroutine(movementCoroutine);
    }

    IEnumerator SpiderDeath()
    {
        yield return new WaitForSeconds(spiderDeathDelay);
        Destroy(gameObject);
    }
}
