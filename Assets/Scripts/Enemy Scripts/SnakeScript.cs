using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeScript : MonoBehaviour
{
    [SerializeField] private GameObject snakeVenom;

    private float moveSpeed = 1.2f;
    private float direction = 1.0f;

    private Vector3 moveDirection = Vector3.right;
    private Vector3 originPosition;
    private Vector3 movePosition;

    public LayerMask playerLayer;
    private void Start()
    {
        originPosition = transform.position;
        originPosition.x += 2.5f;

        movePosition = transform.position;
        movePosition.x -= 2.5f;
    }

    
    private void Update()
    {
        MoveSnake();
        SpitVenom();
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == Tags.BULLET)
        {
            StartCoroutine(Killed(0.2f));
        }
    }

    private void MoveSnake()
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

    private void SpitVenom()
    {
        if (Physics2D.Raycast(transform.position, moveDirection == Vector3.left ? Vector2.left : Vector2.right, 2f, playerLayer))
        {
            StartCoroutine(Spit());
        }
    }

    IEnumerator Killed(float timer)
    {
        yield return new WaitForSeconds(timer);
        Destroy(gameObject);
    }

    IEnumerator Spit()
    {
        yield return new WaitForSeconds(1.5f);
        GameObject venom = Instantiate(snakeVenom, new Vector3(moveDirection == Vector3.left ? transform.position.x - 0.3f : transform.position.x + 0.45f, transform.position.y + 0.2f, transform.position.z), Quaternion.identity);
        venom.GetComponent<SpitVenomScript>().Speed *= transform.localScale.x;

        StartCoroutine(DisableVenom(2f, venom));
    }

    IEnumerator DisableVenom(float timer, GameObject venomSpit)
    {
        yield return new WaitForSeconds(timer);
        Destroy(venomSpit);
    }
}
