using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeScript : MonoBehaviour
{
    [SerializeField] private GameObject snakeVenom;
    [SerializeField] private AudioClip enemyKilledSFX;
    [SerializeField] private AudioClip venomSpitSFX;

    private int score;
    private int snakePoints = 10;
    private float moveSpeed = 1.2f;
    private float direction = 1.0f;
    private float timer;
    private float snakePositionOffset = 2.5f;
    private float snakeDeathDelay = 0.2f;
    private float snakeLeftOffset = 0.3f;
    private float snakeRightOffset = 0.45f;
    private float snakeHeightOffset = 0.2f;
    private float venomDelay = 2f;

    private Vector3 moveDirection = Vector3.right;
    private Vector3 originPosition;
    private Vector3 movePosition;
    private AudioSource audio;

    public LayerMask playerLayer;

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }
    private void Start()
    {
        originPosition = transform.position;
        originPosition.x += snakePositionOffset;

        movePosition = transform.position;
        movePosition.x -= snakePositionOffset;

        timer = 0;
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
            score = snakePoints;
            GameManger.instance.IncreaseScore(score);
            audio.PlayOneShot(enemyKilledSFX, 0.6f);
            StartCoroutine(Killed(snakeDeathDelay));
        }

        if (target.tag == Tags.PLAYER)
        {
            GameManger.instance.LooseLife();
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
        timer += Time.deltaTime;

        if (Physics2D.Raycast(transform.position, moveDirection == Vector3.left ? Vector2.left : Vector2.right, Mathf.Infinity, playerLayer))
        {
            if (timer > 1f)
            {
                GameObject venom = Instantiate(snakeVenom, new Vector3(moveDirection == Vector3.left ? transform.position.x - snakeLeftOffset : transform.position.x + snakeRightOffset, transform.position.y + snakeHeightOffset, transform.position.z), Quaternion.identity);
                venom.GetComponent<SpitVenomScript>().Speed *= transform.localScale.x;
                audio.PlayOneShot(venomSpitSFX, 0.6f);

                StartCoroutine(DisableVenom(venomDelay, venom));
                timer = 0;
            }
        }
    }

    IEnumerator Killed(float timer)
    {
        yield return new WaitForSeconds(timer);
        Destroy(gameObject);
    }

    IEnumerator DisableVenom(float timer, GameObject venomSpit)
    {
        yield return new WaitForSeconds(timer);
        Destroy(venomSpit);
    }
}
