using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exploding_Weasel : MonoBehaviour
{
    private Rigidbody2D rb;

    [Header("Weasel Patrolling")]
    public Transform PointA;
    public Transform PointB;

    private Transform CurrentTarget;
    public float MoveSpeed;
    public float PointRadius;

    [Header("Weasel Idle")]
    public float IdleDuration;
    public float IdleTimer;

    [Header("Weasel Attack")]
    private int Damage = 1;
    public BoxCollider2D AttackArea;
    public float AttackCoolDown;
    private float AttackTimer;
    private bool isAttack;
    private float AttackIdle = 0;

    [Header("Weasel Explode")]
    public CircleCollider2D ExplosionArea;
    public float ExplosionDelay;
    private float ExplosionTimer;
    private bool isPlayerinExplosion;

    private Player_HealSystem PlayerHealth;
    private BoxCollider2D PlayerCollider;
    private Enemy_Health Health;
    
    // Start is called before the first frame update
    void Start()
    {
        PlayerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_HealSystem>();
        PlayerCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider2D>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        Health = gameObject.GetComponent<Enemy_Health>();

        transform.position = PointA.transform.position;
        CurrentTarget = PointB.transform;

        AttackTimer = 0;
        IdleTimer = IdleDuration;
        ExplosionTimer = ExplosionDelay;

        ExplosionArea.enabled = false;
    }

    // Update is called once per frame
    private void Update()
    {
        WeaselDirection();

        WeaselAttack();

        WeaselExplode();
    }


    private void FixedUpdate()
    {
        WeaselPatrolling();
    }


    private void WeaselPatrolling()
    {
        if (CurrentTarget == PointB.transform)
        {
            rb.velocity = new Vector2(MoveSpeed, 0);
        }
        if (CurrentTarget == PointA.transform)
        {
            rb.velocity = new Vector2(-MoveSpeed, 0);
        }

        if (IdleTimer < IdleDuration || AttackIdle < 0)
        {
            rb.velocity = Vector2.zero;
        }
    }


    private void WeaselDirection()
    {
        if (Vector2.Distance(transform.position, CurrentTarget.position) < PointRadius && CurrentTarget == PointB.transform)
        {
            IdleTimer -= Time.deltaTime;
            Debug.Log("Change Direction");

            if (IdleTimer <= 0)
            {
                CurrentTarget = PointA.transform;
                WeaselFlip();

                IdleTimer = IdleDuration;
            }
        }
        if (Vector2.Distance(transform.position, CurrentTarget.position) < PointRadius && CurrentTarget == PointA.transform)
        {
            IdleTimer -= Time.deltaTime;

            if (IdleTimer <= 0)
            {
                CurrentTarget = PointB.transform;
                WeaselFlip();

                IdleTimer = IdleDuration;
            }
        }
    }


    private void WeaselAttack()
    {
        isAttack = AttackArea.IsTouching(PlayerCollider);

        AttackTimer -= Time.deltaTime;
        if (isAttack)
        {
            AttackIdle -= Time.deltaTime;
            if (AttackIdle < 0)
            {
                if (AttackTimer <= 0)
                {
                    PlayerHealth.Damage(Damage);
                    AttackTimer = AttackCoolDown;
                }
            }
        }
        else
        {
            AttackIdle = 0;
        }
    }


    private void WeaselExplode()
    {
        if (Health.EnemyisDead)
        {
            ExplosionArea.enabled = true;
            isPlayerinExplosion = ExplosionArea.IsTouching(PlayerCollider);
            ExplosionTimer -= Time.deltaTime;

            if (ExplosionTimer <= 0)
            {
                if (isPlayerinExplosion)
                {
                    PlayerHealth.Damage(Damage);

                    ExplosionArea.enabled = false;
                    GameObject.Destroy(gameObject);
                }
                else
                {
                    ExplosionArea.enabled = false;
                    GameObject.Destroy(gameObject);
                }
            }
        }
    }


    private void WeaselFlip()
    {
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(PointA.transform.position, PointRadius);
        Gizmos.DrawWireSphere(PointB.transform.position, PointRadius);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerHealth.Damage(1);
        }
    }
}
