using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject player;
    public Rigidbody2D rb;

    [Header("Movment")]
    private float Horizontal;
    public float movespeed = 5;
    public float JumpForce;
    public float GravityScale = 10;
    public float FallingGravityScale = 40;

    [Header("GroundCheck")]
    public bool GroundCheck = false;
    public float GroundLine = 1.5f;
    public LayerMask GroundLayer;

    [Header("jump Window")]
    private bool Jumping;
    public float ButtonTime = 0.23f;
    private float JumpTime;

    [Header("Fogiving The Player")]
    private float CoyoteCounter;
    private float JumpBufferCounter;

    [Header("DoubleJump")]
    public float DoubleJumpCount = 0;
    public float DoubleJumpForce;

    [Header("Wall Sliding")]
    public bool isWallSliding;
    public float WallSlidingSpeed = 2f;
    public Transform WallCheck;
    public LayerMask WallLayer;

    [Header("Wall Jumping")]
    public bool isWallJumping;
    private float WallJumpingDirection;
    private float WallJumpingTime = 0.2f;
    private float WallJumpingCounter;
    private float WallJumpingDuration = 0.2f;
    private Vector2 WallJumpingPower = new Vector2(80f, 180f);

    [Header("Flip")]
    public bool isFacingRight = true;

    [Header("Dash")]
    public float DashPower = 2500;
    public float DashCoolDown = 0.4f;
    private float DashDir;
    private bool dashing;
    private bool CanDash = true;
    public float dashTime = 0.2f;
    public TrailRenderer Tr;
    
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

        // DoubleJump

        DoubleJump();

        // WallSlide

        WallSlide();

        // WallJump

        WallJump();

        // Flip

        if (!isWallJumping)
        {
            Flipe();
        }

        //dash
        if (isFacingRight)
        {
            DashDir = 1;
        }
        if (isFacingRight == false)
        {
            DashDir = -1;
        }
    }

    void FixedUpdate()
    {
        // Moving
        if (!isWallJumping)
        {
            rb.velocity = new Vector2(Horizontal * movespeed, rb.velocity.y);
        }

        // Changing gravity
        if (rb.velocity.y >= 0)
        {
            rb.gravityScale = GravityScale;
        }
        else if (rb.velocity.y < 0)
        {
            rb.gravityScale = FallingGravityScale;
        }

        //dash
        if (Input.GetKeyDown(KeyCode.LeftShift) && CanDash)
        {
            StartCoroutine(Dash());
        }
        if (dashing)
        {
            return;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * GroundLine);
    }

    private void DoubleJump()
    {
        if (GroundCheck && Input.GetKeyDown(KeyCode.Space))
        {
            DoubleJumpCount = 1;
        }
        if (!GroundCheck && Input.GetKeyDown(KeyCode.Space) && DoubleJumpCount > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, DoubleJumpForce);
            DoubleJumpCount = 0;
        }
    }

    private bool isWalled()
    {
        return Physics2D.OverlapCircle(WallCheck.position, 0.2f, WallLayer);
    }

    private void WallSlide()
    {
        if (isWalled() && !GroundCheck && Horizontal != 0)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -WallSlidingSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }
    }

    private void WallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            WallJumpingDirection = -transform.localScale.x;
            WallJumpingCounter = WallJumpingTime;

            CancelInvoke(nameof(StopWallJump));
        }
        else
        {
            WallJumpingCounter -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space) && WallJumpingCounter > 0) 
        {
            isWallJumping = true;
            rb.velocity = new Vector2(WallJumpingDirection * WallJumpingPower.x, WallJumpingPower.y);
            WallJumpingCounter = 0f;

            if (transform.localScale.x != WallJumpingDirection)
            {
                if (Horizontal < 0f)
                {
                    isFacingRight = !isFacingRight;
                    transform.rotation = Quaternion.Euler(0, -180, 0);
                }
                if (Horizontal > 0f)
                {
                    isFacingRight = !isFacingRight;
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }
            }

            Invoke(nameof(StopWallJump), WallJumpingDuration);
        }

    }

    private void StopWallJump()
    {
        isWallJumping = false;
    }

    private IEnumerator Dash()
    {
        CanDash = false;
        dashing = true;
        float OriginalGravity = rb.gravityScale;
        rb.AddForce(new Vector2(DashDir * DashPower, 0f), ForceMode2D.Impulse);
        rb.gravityScale = 0;
        Tr.emitting = true;
        yield return new WaitForSeconds(dashTime);
        Tr.emitting = false;
        rb.gravityScale = OriginalGravity;
        dashing = false;
        yield return new WaitForSeconds(DashCoolDown); ;
        CanDash = true;
    }

    public void Flipe()
    {
        if (Horizontal < 0f && isFacingRight)
        {
            isFacingRight = false;
            transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        if (Horizontal > 0f && isFacingRight == false)
        {
            isFacingRight = true;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public void InstantFlipe()
    {
        if (isFacingRight)
        {
            isFacingRight = false;
            transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        if (isFacingRight == false)
        {
            isFacingRight = true;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
