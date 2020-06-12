using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorpionScript : MonoBehaviour
{
    [SerializeField] private AudioClip enemyKilledSFX;

    private int score;
    private int scorpionPoints = 8;
    private float moveSpeed = 2.5f;
    private float direction = -1.0f;
    private float scorpionPositionOffset = 3f;
    private float scorpionDeathDelay = 0.2f;

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
        originPosition.x += scorpionPositionOffset;

        movePosition = transform.position;
        movePosition.x -= scorpionPositionOffset;
    }

    
    private void Update()
    {
        MoveScorpion();
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == Tags.BULLET)
        {
            score = scorpionPoints;
            GameManger.instance.IncreaseScore(score);
            audio.PlayOneShot(enemyKilledSFX, 0.6f);
            StartCoroutine(Killed(scorpionDeathDelay));
        }
    }

    private void MoveScorpion()
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
