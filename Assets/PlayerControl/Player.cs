using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject player;
    public Rigidbody2D rb;
    private float Horizontal;
    public float movespeed = 5;
    public float JumpForce;
    public float GravityScale = 10;
    public float FallingGravityScale = 40;
    [Header("GroundCheck")]
    public bool GroundCheck = false;
    public float GroundLine = 1.5f;
    public LayerMask GroundLayer;
    //jump window
    private bool Jumping;
    private float ButtonTime = 0.23f;
    private float JumpTime;
    // Forgiving the player
    private float CoyoteCounter;
    private float JumpBufferCounter;
    // DoubleJump
    public float DoubleJump = 0;
    public float DoubleJumpForce;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");

        GroundCheck = Physics2D.Raycast(transform.position, Vector2.down, GroundLine, GroundLayer);


        // Jumping

        if (GroundCheck)
        {
            CoyoteCounter = 0.1f;
        }
        else
        {
            CoyoteCounter -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpBufferCounter = 0.1f;
        }
        else
        {
            JumpBufferCounter -= Time.deltaTime;
        }

        if (JumpBufferCounter > 0f && CoyoteCounter > 0f)
        {
            Jumping = true;
            JumpTime = 0;
            JumpBufferCounter = 0;
        }

        if (Jumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, JumpForce);
            JumpTime += Time.deltaTime;
        }

        if (Input.GetKeyUp(KeyCode.Space) | JumpTime > ButtonTime) 
        {
            Jumping = false;
            CoyoteCounter = 0;
        }

        if (GroundCheck && Input.GetKeyDown(KeyCode.Space))
        {
            DoubleJump = 1;
        }
        if (!GroundCheck && Input.GetKeyDown(KeyCode.Space) && DoubleJump > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, DoubleJumpForce);
            DoubleJump = 0;
        }
    }

    void FixedUpdate()
    {
        // Moving
        rb.velocityX = Horizontal * movespeed * Time.deltaTime;
       
        // Changing gravity
        if (rb.velocity.y >= 0)
        {
            rb.gravityScale = GravityScale;
        }
        else if (rb.velocity.y < 0)
        {
            rb.gravityScale = FallingGravityScale;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * GroundLine);
    }
}
