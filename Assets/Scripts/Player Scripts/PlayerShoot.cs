using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private GameObject fireBullet;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        
    }

    
    private void Update()
    {
        ShootBullet();
    }

    private void ShootBullet()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            GameObject bullet = Instantiate(fireBullet, new Vector3(transform.position.x, transform.position.y - 0.2f, transform.position.z), Quaternion.identity);
            bullet.GetComponent<FireBullet>().Speed *= transform.localScale.x;
            anim.SetBool("Shooting", true);
        }

        anim.SetBool("Shooting", false);
    }
}
