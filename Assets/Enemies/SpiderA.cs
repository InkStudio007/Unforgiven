using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderA : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform Target;
    public Transform SiteOfViewPosition;
    private Player_HealSystem Player_Health;

    [Header("Follow Target")]
    public float SiteOfView;
    private float DistanceFromTarget;
    private Vector2 TargetDirection;
    public float FollowMoveSpeed;
    public float Acceleration = 1f;
    public float AccelerateSpeed;

    [Header("Spider Patrolling")]
    public Transform PointA;
    public Transform PointB;

    private Transform CurrentTarget;
    public float MoveSpeed;
    public float PointRadius;
    private bool Patrol;
    private Vector2 PatrolDirection;

    [Header("Spider Idle")]
    public float IdleDuration;
    public float IdleTimer;
    public float StopFollowingDuration;
    private float StopFollowingTimer;

    [Header("Spider Attack")]
    public Transform AttackArea;
    public float AttackRadius;
    public int Damage;
    public float JumpForce;
    public float FallingGravity;
    private float Gravity;
    public float JumpCoolDown;
    private float JumpTimer;
    public float AttackCoolDown;
    private float AttackTimer;

    [Header("GroundCheck")]
    public bool GroundCheck = false;
    public float GroundLine = 1.5f;
    public LayerMask GroundLayer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        Player_Health = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_HealSystem>();

        SiteOfViewPosition.position = new Vector3(transform.position.x + (SiteOfView / 2), transform.position.y, transform.position.z);
        AttackArea.position = new Vector3(transform.position.x + (AttackRadius / 2), transform.position.y, transform.position.z);

        transform.position = PointA.transform.position;
        CurrentTarget = PointB.transform;

        IdleTimer = IdleDuration;
        StopFollowingTimer = StopFollowingDuration;
        JumpTimer = 0;
        AttackTimer = 0;

        Patrol = true;

        Gravity = rb.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        GroundCheck = Physics2D.Raycast(transform.position, Vector2.down, GroundLine, GroundLayer);

        SpiderAcceleration();
        SpiderDirection();
    }

    void FixedUpdate()
    {
        SpiderFollowTarget();
        SpiderPatrolling();
        SpiderAttack();
   
    }

    private void SpiderFollowTarget()
    {
        DistanceFromTarget = Vector2.Distance(transform.position, Target.transform.position);
        TargetDirection = (Target.position - transform.position).normalized;

        if (DistanceFromTarget < SiteOfView)
        {
            rb.velocity = new Vector2(TargetDirection.x * Acceleration, rb.velocityY);
            Patrol = false;
            IdleTimer = IdleDuration;

            FlipBaseOnTarget();
        }
        else
        {
            Acceleration = 0f;

            if (Patrol == false)
            {
                StopFollowingTimer -= Time.deltaTime;

                if (StopFollowingTimer <= 0)
                {
                    CurrentTarget = PointA.transform;
                    StopFollowingTimer = StopFollowingDuration;
                    FlipOnPatrol();

                    Patrol = true;
                }
            }
        }
    }

    private void SpiderAcceleration()
    {
        if (Patrol == false)
        {
            if (Acceleration < FollowMoveSpeed)
            {
                Acceleration += Time.deltaTime * AccelerateSpeed;
            }
        }
        if (Patrol)
        {
            Acceleration = 0;
        }
    }

    private void SpiderPatrolling()
    {
        PatrolDirection = (CurrentTarget.position - transform.position).normalized;

        if (Patrol)
        {
            rb.velocity = new Vector2(PatrolDirection.x * MoveSpeed, rb.velocityY);
            
            if (IdleTimer < IdleDuration)
            {
                rb.velocity = Vector2.zero;
            }
        }
        
    }


    private void SpiderDirection()
    {
        if (Patrol)
        {
            if (Vector2.Distance(transform.position, CurrentTarget.position) < PointRadius && CurrentTarget == PointB.transform)
            {
                IdleTimer -= Time.deltaTime;

                if (IdleTimer <= 0)
                {
                    CurrentTarget = PointA.transform;
                    FlipOnPatrol();

                    IdleTimer = IdleDuration;
                }
            }
            if (Vector2.Distance(transform.position, CurrentTarget.position) < PointRadius && CurrentTarget == PointA.transform)
            {
                IdleTimer -= Time.deltaTime;

                if (IdleTimer <= 0)
                {
                    CurrentTarget = PointB.transform;
                    FlipOnPatrol();

                    IdleTimer = IdleDuration;
                }
            }
        }    
    }

    private void SpiderAttack()
    {
        JumpTimer -= Time.deltaTime;
        AttackTimer -= Time.deltaTime;

        if (DistanceFromTarget < AttackRadius)
        {
            if (JumpTimer <= 0)
            {
                rb.AddForce(new Vector2(JumpForce * 2, JumpForce), ForceMode2D.Impulse);
                JumpTimer = JumpCoolDown;
            }
        }

        if (rb.velocity.y < 0)
        {
            rb.gravityScale = FallingGravity;
        }

        if (GroundCheck)
        {
            rb.gravityScale = Gravity;
        }

    }

    private void FlipOnPatrol()
    {
        if (Patrol)
        {
            if (CurrentTarget == PointA)
            {
                transform.rotation = Quaternion.Euler(transform.rotation.x, 0f, transform.rotation.z);
            }
            if (CurrentTarget == PointB)
            {
                transform.rotation = Quaternion.Euler(transform.rotation.x, 180f, transform.rotation.z);
            }
        }
        else
        {
            if (PatrolDirection.x > 0)
            {
                transform.rotation = Quaternion.Euler(transform.rotation.x, 180f, transform.rotation.z);
            }
            if (PatrolDirection.x < 0)
            {
                transform.rotation = Quaternion.Euler(transform.rotation.x, 0f, transform.rotation.z);
            }
        }
    }

    private void FlipBaseOnTarget()
    {
        if (TargetDirection.x > 0)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, 180f, transform.rotation.z);
        }
        if (TargetDirection.x < 0)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, 0f, transform.rotation.z);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (AttackTimer <= 0)
            {
                Player_Health.Damage(Damage);
                AttackTimer = AttackCoolDown;
            }
        }  
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(SiteOfViewPosition.position, new Vector3(SiteOfView, 20, 0f));
        Gizmos.DrawWireCube(AttackArea.position, new Vector3(AttackRadius, 40, 0f));

        Gizmos.DrawWireSphere(PointA.transform.position, PointRadius);
        Gizmos.DrawWireSphere(PointB.transform.position, PointRadius);

        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * GroundLine);
    }
}
