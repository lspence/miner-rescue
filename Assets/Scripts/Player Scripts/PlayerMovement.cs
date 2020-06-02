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
    private float flyingPower = 125f;
    private float hoverPower = 0.3f;
    private float downVelocity = -3.75f;
    private float groundCheckDistance = 0.1f;
    private float wallCheckDistance = 0.5f;
    private bool isGrounded;
    private bool isTouchingWall;
    private bool jumped;
    private bool flying;
    private bool isFacingRight;

    private Rigidbody2D myBody;
    private Animator anim;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        myBody.gravityScale = 25f;
    }


    private void FixedUpdate()
    {
        Run();
    }


    private void Update()
    {
        ResetGravity();
        CheckOnGround();
        CheckTouchingWall();
        Jump();
    }

    private void ResetGravity()
    {
        myBody.gravityScale = 1f;
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

        if (!isGrounded)
        {
            flying = true;
            myBody.velocity = new Vector2(myBody.velocity.x, hoverPower);
            anim.SetBool("Flying", true);
        }
        else
        {
            GameManger.instance.GetComponent<AudioSource>().Play();
        }


        if (Input.GetKey(KeyCode.DownArrow))
        {
            flying = true;
            myBody.velocity = new Vector2(myBody.velocity.x, downVelocity);
            anim.SetBool("Flying", true);

            if (isGrounded)
            {
                flying = false;
                anim.SetBool("Flying", false);
            }
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
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                jumped = true;
                myBody.velocity = new Vector2(myBody.velocity.x, jumpPower);
                anim.SetBool("Jump", true);
                GameManger.instance.GetComponent<AudioSource>().Play();
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                flying = true;
                myBody.AddForce(new Vector2(myBody.velocity.x, flyingPower), ForceMode2D.Force);
                anim.SetBool("Flying", true);
            }
        }
    }
}
