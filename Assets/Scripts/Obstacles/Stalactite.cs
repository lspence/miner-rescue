using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stalactite : MonoBehaviour
{
    [SerializeField] private Transform bottomCollision;
    private float targetRadius = 1.5f;
    private float delayTime = 0.5f;
    private float removeObstacleDelay = 1.1f;

    public LayerMask playerLayer;

    private Rigidbody2D myBody;
    private Animator anim;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        
    }

    
    private void Update()
    {
        Collider2D target = Physics2D.OverlapCircle(bottomCollision.position, targetRadius, playerLayer);

        if (target != null)
        {
            anim.Play("Shaking");
            StartCoroutine(DropDelay(delayTime));
            StartCoroutine(RemoveObstacle(removeObstacleDelay));
        }
    }

    IEnumerator DropDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        myBody.bodyType = RigidbodyType2D.Dynamic;
    }

    IEnumerator RemoveObstacle(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
