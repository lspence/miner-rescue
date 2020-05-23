using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private AudioClip hoverSFX;
    [SerializeField] private AudioClip shootSFX;
    [SerializeField] private AudioClip jumpSFX;

    public LayerMask groundLayer;

    private int direction = 1;
    private float speed = 5f;
    private float jumpPower = 3.5f;
    private float flyingPower = 18.6f;
    private float groundCheckDistance = 0.1f;
    private float wallCheckDistance = 0.5f;
    private bool isGrounded;
    private bool isTouchingWall;
    private bool jumped;
    private bool flying;
    private bool isFacingRight;

    private Rigidbody2D myBody;
    private Animator anim;
    private AudioSource audio;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        Run();
    }


    private void Update()
    {
        CheckOnGround();
        CheckTouchingWall();
        Jump();
        Flying();
    }

    private void Run()
    {
        float hAxis = Input.GetAxisRaw("Horizontal");

        if (hAxis > 0)
        {
            myBody.velocity = new Vector2(speed, myBody.velocity.y);
            FlipDirection(direction);
            isFacingRight = true;
        }
        else if (hAxis < 0)
        {
            myBody.velocity = new Vector2(-speed, myBody.velocity.y);
            FlipDirection(-direction);
            isFacingRight = false;
        }
        else
        {
            myBody.velocity = new Vector2(0f, myBody.velocity.y);
        }

        anim.SetInteger("Speed", Mathf.Abs((int)myBody.velocity.x));
    }

    private void FlipDirection(int direction)
    {
        Vector3 tempScale = transform.localScale;
        tempScale.x = direction;
        transform.localScale = tempScale;
    }

    private void CheckOnGround()
    {
        isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundLayer);

        if (isGrounded && jumped)
        {
            jumped = false;
            anim.SetBool("Jump", false);
        }

        if (isGrounded && flying)
        {
            flying = false;
            anim.SetBool("Flying", false);
        }
    }

    private void CheckTouchingWall()
    {
        isTouchingWall = isFacingRight == true ? Physics2D.Raycast(wallCheck.position, Vector2.right, wallCheckDistance, groundLayer) : Physics2D.Raycast(wallCheck.position, Vector2.left, wallCheckDistance, groundLayer);
    }
    
    private void Jump()
    {
        if (isGrounded && !isTouchingWall)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                jumped = true;
                myBody.velocity = new Vector2(myBody.velocity.x, jumpPower);
                anim.SetBool("Jump", true);
                audio.PlayOneShot(jumpSFX, 0.6f);
            }
        }
    }

    private void Flying()
    {
        if (Input.GetKey(KeyCode.H)) // Hovering
        {
            flying = true;
            myBody.AddForce(new Vector2(myBody.velocity.x, flyingPower));
            anim.SetBool("Flying", true);

            //if (!audio.isPlaying)
            //{
            //    audio.clip = hoverSFX;
            //    audio.Play();
            //}
            //else
            //{
            //    audio.Stop();
            //}
        }

        if (Input.GetKey(KeyCode.H) && Input.GetKeyDown(KeyCode.F)) // Hovering
        {
            flying = true;
            myBody.AddForce(new Vector2(myBody.velocity.x, flyingPower));
            anim.SetBool("Flying", true);
            audio.PlayOneShot(shootSFX, 0.6f);
        }
    }
}
