using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderScript : MonoBehaviour
{
    private string movementCoroutine = "ChangeMovementDirection";

    private int score;

    private Rigidbody2D myBody;

    private Vector3 moveDirection = Vector3.down;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
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

    IEnumerator ChangeMovementDirection()
    {
        yield return new WaitForSeconds(Random.Range(0.09f, 0.145f));

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
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == Tags.BULLET)
        {
            score = 5;
            GameManger.instance.IncreaseScore(score);
            myBody.bodyType = RigidbodyType2D.Dynamic;
            StartCoroutine(SpiderDeath());
            StopCoroutine(movementCoroutine);
        }

        if (target.tag == Tags.PLAYER)
        {
            GameManger.instance.LooseLife();
        }
    }
}
