using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private GameObject fireBullet;
    [SerializeField] private AudioClip shootSFX;

    private float bulletYOffset = 0.2f;

    private Animator anim;
    private AudioSource audio;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
    }
        
    private void Update()
    {
        ShootBullet();
    }

    private void ShootBullet()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject bullet = Instantiate(fireBullet, new Vector3(transform.position.x, transform.position.y - bulletYOffset, transform.position.z), Quaternion.identity);
            bullet.GetComponent<FireBullet>().Speed *= transform.localScale.x;
            anim.SetBool("Shooting", true);
            audio.PlayOneShot(shootSFX, 0.6f);
        }
        
        anim.SetBool("Shooting", false);
    }
}
