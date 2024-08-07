using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    private Rigidbody2D rb;
    private Player_HealSystem PlayerHeath;
    private Transform Target;
    public Transform OrgPosition;
    public Transform AttackAreaCenter;
    private Enemy_Health Health;


    [Header("Enemy PathFinding")]
    private Vector3 TargetDirection;
    private Vector3 TargetPosition;
    public float MoveSpeed;
    public float LineOfSite;
    private bool SawPlayer = false;
    private Vector3 LastPoint;
    private bool ReachedLastPoint;
    public float SearchDuration;
    private float SearchTimer;
    public float SearchSpeed;
    private bool ReachedOrgPosition;
    private float DistanceFromTarget;
    private Vector3 OrgPositionDirection;

    [Header("Enemy Attack")]
    public float AttackAreaRadius;
    public int AttackDamage;
    private float AttackAreaAndTargetDistance;
    public float AttackCoolDowm;
    private float AttackTimer;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        PlayerHeath = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_HealSystem>();
        Health = gameObject.GetComponent<Enemy_Health>();
        Target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        TargetPosition = Target.transform.position + Vector3.up;
        LastPoint = Vector3.zero;
        SawPlayer = false;
        ReachedOrgPosition = false;
        SearchTimer = SearchDuration;
        AttackTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        TargetPosition = Target.transform.position + Vector3.up;
        TargetDirection = (TargetPosition - transform.position).normalized;
        OrgPositionDirection = (OrgPosition.position - transform.position).normalized;


        Flip();
        EnemyAttack();


        if (Health.EnemyisDead)
        {
            GameObject.Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        FollowTarget();
        BackToOrgPosition();
    }

    private void FollowTarget()
    {
        DistanceFromTarget = Vector2.Distance(transform.position, Target.transform.position);

        if (DistanceFromTarget < LineOfSite)
        {
            SawPlayer = true;
            LastPoint = Vector3.zero;
            ReachedLastPoint = false;
            ReachedOrgPosition = false;
            SearchTimer = SearchDuration;
        }

        if (LastPoint == Vector3.zero && SawPlayer && DistanceFromTarget < LineOfSite)
        {
            LastPoint = TargetPosition;
        }

        if (LastPoint != Vector3.zero)
        {
            rb.MovePosition(Vector2.MoveTowards(transform.position, LastPoint, MoveSpeed * Time.fixedDeltaTime));
        }

        if (LastPoint != Vector3.zero && Vector2.Distance(transform.position, LastPoint) < 10f && DistanceFromTarget > LineOfSite)
        {
            LastPoint = Vector3.zero;
            ReachedLastPoint = true;
        }
        if (ReachedLastPoint && SearchTimer > 0)
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void BackToOrgPosition()
    {
        if (ReachedLastPoint && !ReachedOrgPosition)
        {
            SearchTimer -= Time.deltaTime;
            if (SearchTimer <= 0)
            {
                rb.MovePosition(Vector2.MoveTowards(transform.position, OrgPosition.position, SearchSpeed * Time.fixedDeltaTime));
            }
        }
        if (!ReachedLastPoint || ReachedOrgPosition)
        {
            SearchTimer = SearchDuration;
        }

        if (SearchTimer <= 0 && Vector2.Distance(transform.position, OrgPosition.position) < 10f)
        {
            ReachedOrgPosition = true;
        }

        if (ReachedOrgPosition)
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void EnemyAttack()
    {
        AttackAreaAndTargetDistance = Vector2.Distance(AttackAreaCenter.position, Target.position);
        AttackTimer -= Time.deltaTime;

        if (AttackAreaAndTargetDistance < AttackAreaRadius)
        {
            if (AttackTimer <= 0)
            {
                PlayerHeath.Damage(AttackDamage);
                AttackTimer = AttackCoolDowm;
            }
        }
    }
    private void Flip()
    {
        if (DistanceFromTarget < LineOfSite)
        {
            if (TargetDirection.x > 0)
            {
                transform.rotation = Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z);
            }
            if (TargetDirection.x < 0)
            {
                transform.rotation = Quaternion.Euler(transform.rotation.x, 180, transform.rotation.z);
            }
        }   

        if (ReachedLastPoint && !ReachedOrgPosition)
        {
            if (SearchTimer <= 0)
            {
                if (OrgPositionDirection.x > 0)
                {
                    transform.rotation = Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z);
                }
                if (OrgPositionDirection.x < 0)
                {
                    transform.rotation = Quaternion.Euler(transform.rotation.x, 180, transform.rotation.z);
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (AttackTimer <= 0)
            {
                PlayerHeath.Damage(AttackDamage);
                AttackTimer = AttackCoolDowm;
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(this.transform.position, LineOfSite);
        Gizmos.DrawWireSphere(LastPoint, 10f);
        Gizmos.DrawWireSphere(AttackAreaCenter.position, AttackAreaRadius);
        
        Gizmos.color = Color.red;
    }
}
