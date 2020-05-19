using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatScript : MonoBehaviour
{
    private int score;
    private float moveSpeed = 2f;
    private float direction = 1.0f;

    private Vector3 moveDirection = Vector3.right;
    private Vector3 originPosition;
    private Vector3 movePosition;


    private void Start()
    {
        originPosition = transform.position;
        originPosition.x += 2f;

        movePosition = transform.position;
        movePosition.x -= 2f;
    }

    
    private void Update()
    {
        MoveBat();
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == Tags.BULLET)
        {
            score = 2;
            GameManger.instance.IncreaseScore(score);
            StartCoroutine(Killed(0.2f));
        }

        if (target.tag == Tags.PLAYER)
        {
            GameManger.instance.LooseLife();
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
