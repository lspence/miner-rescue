using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;

    public LayerMask groundLayer;

    private int direction = 1;
    private float speed = 5f;
    private float jumpPower = 3.5f;
    private float flyingPower = 10;
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
        
    }

    private void FixedUpdate()
    {
        Run();
    }


    private void Update()
    {
        //Debug.DrawRay(groundCheck.position, Vector2.down * 0.5f, Color.yellow); //Visually show raycast in scene view.
        //Debug.DrawRay(wallCheck.position, Vector2.right * 0.5f, Color.yellow); //Visually show raycast in scene view.

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
        isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, 0.1f, groundLayer);

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
        isTouchingWall = isFacingRight == true ? Physics2D.Raycast(wallCheck.position, Vector2.right, 0.5f, groundLayer) : Physics2D.Raycast(wallCheck.position, Vector2.left, 0.5f, groundLayer);
    }
    
    private void Jump()
    {
        if (isGrounded && !isTouchingWall)
        {
            Debug.Log("On ground: " + isGrounded);
            if (Input.GetKey(KeyCode.Space))
            {
                jumped = true;
                myBody.velocity = new Vector2(myBody.velocity.x, jumpPower);
                anim.SetBool("Jump", true);
            }
        }
    }

    private void Flying()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            flying = true;
            myBody.AddForce(new Vector2(myBody.velocity.x, flyingPower));
            anim.SetBool("Flying", true);
        }
    }
}
