using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatScript : MonoBehaviour
{
    private int score;
    private float moveSpeed = 2.5f;
    private float direction = 1.0f;

    private Vector3 moveDirection = Vector3.right;
    private Vector3 originPosition;
    private Vector3 movePosition;

    private void Start()
    {
        originPosition = transform.position;
        originPosition.x += 5f;

        movePosition = transform.position;
        movePosition.x -= 5f;
    }

    
    private void Update()
    {
        MoveRat();
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == Tags.BULLET)
        {
            score = 1;
            GameManger.instance.IncreaseScore(score);
            StartCoroutine(Killed(0.2f));
        }

        if (target.tag == Tags.PLAYER)
        {
            GameManger.instance.LooseLife();
        }
    }

    private void MoveRat()
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
