using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
    public float Speed { get; set; } = 10f;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        StartCoroutine(DisableBullet(1f));
    }

    
    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 temp = transform.position;
        temp.x += Speed * Time.deltaTime;
        transform.position = temp;
    }

    IEnumerator DisableBullet(float timer)
    {
        yield return new WaitForSeconds(timer);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.tag == Tags.BAT || target.gameObject.tag == Tags.RAT || target.gameObject.tag == Tags.SNAKE || target.gameObject.tag == Tags.SPIDER)
        {
            anim.Play("Fire");
            StartCoroutine(DisableBullet(0.1f));
        }
    }
}
