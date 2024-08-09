using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderB : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform Target;
    private Player_HealSystem Player_Health;

    [Header("Shoot")]
    public Transform ShootPoint;
    public GameObject SpiderWeb;
    public float ShootingCoolDown;
    private float ShootingTimer = 0f;
    [SerializeField] int Damage;

    [Header("LineOfSite")]
    public Transform LineOfSightPos;
    public float LineOfSight;
    private float TargetDistance;
    private Vector3 TargetDirection;
    private bool PlayerInSight;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        Player_Health = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_HealSystem>();

        LineOfSightPos.position = new Vector3(transform.position.x + (LineOfSight / 2), transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        rb.MovePosition(new Vector3(rb.position.x, rb.position.y, 0));
        TargetDirection = (Target.transform.position - transform.position).normalized;

        FindTarget();

        if (PlayerInSight)
        {
            ShootSpiderWeb();
            Flip();
        }
        
    }

    private void ShootSpiderWeb()
    {
        ShootingTimer -= Time.deltaTime;
        if (ShootingTimer <= 0)
        {
            Instantiate(SpiderWeb, ShootPoint.position, rotation: Quaternion.identity);
            ShootingTimer = ShootingCoolDown;
        }
    }

    private void FindTarget()
    {
        TargetDistance = Vector3.Distance(transform.position, Target.transform.position);

        if (TargetDistance < LineOfSight)
        {
            PlayerInSight = true;
        }
        else
        {
            PlayerInSight = false;
        }
    }

    private void Flip()
    {
        if(TargetDirection.x > 0)
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
        if (collision.gameObject.tag == "Player")
        {
            Player_Health.Damage(Damage);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(LineOfSightPos.transform.position, new Vector3(LineOfSight, 50f, 0));
    }
}
