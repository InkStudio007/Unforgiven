using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HornBear : MonoBehaviour
{
    private Transform Target;
    private Rigidbody2D rb;
    private Player_HealSystem PlayerHealth;

    [Header("Charge Attack")]
    public Transform ChargeAttackAreaPos;
    public Transform ChargeAttackTarget;
    [SerializeField] float ChargeAttackArea;
    [SerializeField] float ChargeAttackCoolDown;
    [SerializeField] float ChargeAttackForce;
    [SerializeField] float MaxChargeDistance;
    private Vector3 TargetDirection;
    private Vector3 ChargeTargetDirection;
    private float DistanceFromTarget;
    public float ChargeAttackTimer;
    public bool isChargeAttack = false;
    public bool CanCharge = false;
    private bool FirstChargeAttack = false;
    [SerializeField] int Damage;
    [SerializeField] float DamageCoolDown;
    private float DamageTimer;

    [Header("Bear Idle")]
    [SerializeField] float IdleDuration;
    [SerializeField] float IdleTimer;

    [Header("Drag Dead Bear")]
    private Enemy_Health Health;
    private bool CanDrag;

    // Start is called before the first frame update
    void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        Health = GetComponent<Enemy_Health>();
        PlayerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_HealSystem>();

        ChargeAttackAreaPos.position = new Vector3(transform.position.x + (ChargeAttackArea / 2), transform.position.y, transform.position.z);

        ChargeAttackTimer = ChargeAttackCoolDown;
        IdleTimer = IdleDuration;
        DamageTimer = 0;

        CanCharge = true;
    }

    // Update is called once per frame
    void Update()
    {
        TargetDirection = (Target.transform.position - transform.position).normalized;
        DistanceFromTarget = Vector3.Distance(transform.position, Target.transform.position);

        DamageTimer -= Time.deltaTime;

        if (FirstChargeAttack == false)
        {
            Flip();
        }

        DragDeadBear();
    }

    private void FixedUpdate()
    {
        if (Health.EnemyisDead == false)
        {
            ChargedAttack();
        }
    }

    private void ChargedAttack()
    {
        if (DistanceFromTarget < ChargeAttackArea)
        {
            if (CanCharge)
            {
                ChargeAttackTimer -= Time.deltaTime;
                if (ChargeAttackTimer <= 0)
                {
                    ChargeTargetDirection = (Target.transform.position - transform.position).normalized;
                    ChargeAttackTarget.transform.position = new Vector3(ChargeAttackTarget.transform.position.x + (MaxChargeDistance * ChargeTargetDirection.x), ChargeAttackTarget.transform.position.y, ChargeAttackTarget.transform.position.z);

                    rb.velocity = new Vector2(ChargeTargetDirection.x * ChargeAttackForce, 0f);

                    ChargeAttackTimer = ChargeAttackCoolDown;

                    isChargeAttack = true; 
                    CanCharge = false;
                    FirstChargeAttack = true;
                }
            }
        }
        else
        {
            ChargeAttackTimer = ChargeAttackCoolDown;
        }

        if (Vector3.Distance(transform.position, ChargeAttackTarget.transform.position) < 10f && isChargeAttack)
        {
            rb.velocity = Vector2.zero;
        }

        if (rb.velocity == Vector2.zero && isChargeAttack)
        {
            IdleTimer -= Time.deltaTime;
            if (IdleTimer <= 0)
            {
                Flip();
                CanCharge = true;
                isChargeAttack = false;
                IdleTimer = IdleDuration;
            }
        }
    }

    private void DragDeadBear()
    {
        if (Health.EnemyisDead)
        {
            rb.velocity = Vector2.zero;
            if(CanDrag)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    this.transform.parent = Target.transform;
                    rb.bodyType = RigidbodyType2D.Dynamic;
                }
                if (Input.GetKeyUp(KeyCode.E))
                {
                    this.transform.parent = null;
                    rb.bodyType = RigidbodyType2D.Kinematic;
                }
            }
            else
            {
                this.transform.parent = null;
                rb.bodyType = RigidbodyType2D.Kinematic;
            }
        }
        else
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }

    private void Flip()
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Health.EnemyisDead)
        {
            CanDrag = true;
        }
        else
        {
            if (DamageTimer <= 0)
            {
                PlayerHealth.Damage(Damage);
                DamageTimer = DamageCoolDown;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (Health.EnemyisDead)
        {
            CanDrag = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(ChargeAttackAreaPos.position, new Vector3(ChargeAttackArea, 40, 0));
        Gizmos.DrawWireSphere(ChargeAttackTarget.transform.position, 10);
    }
}
