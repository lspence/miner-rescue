using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitVenomScript : MonoBehaviour
{
    public float Speed { get; set; } = 2.5f;

    private float venomClearDelay = 2f;
    private void Start()
    {
        StartCoroutine(DisableVenom(venomClearDelay));
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

    IEnumerator DisableVenom(float timer)
    {
        yield return new WaitForSeconds(timer);
        Destroy(gameObject);
    }
}
