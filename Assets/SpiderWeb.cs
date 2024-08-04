using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderWeb : MonoBehaviour
{
    private Transform Target;
    private Rigidbody2D rb;
    private Player Player_Script;
    private Player_HealSystem Player_Health;

    [Header("Point To Target")]
    [SerializeField] float ShootForce;
    private Vector3 TargetDirection;

    [Header("Attack Effect")]
    [SerializeField] float SlownessSpeed;
    [SerializeField] float SlownessAOE;
    public bool isSlownessEffected;
    [SerializeField] int Damage;
    private float PlayerOrgSpeed;
    private float TargetAndSlownessAOEdistance;
    // Start is called before the first frame update
    void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        Player_Script = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        Player_Health = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_HealSystem>();

        PlayerOrgSpeed = 80;
        isSlownessEffected = false;

        SpiderWebPointToTarget();
    }

    void Update()
    {
        SlownessEffect();
    }

    private void SpiderWebPointToTarget()
    {
        TargetDirection = Target.transform.position - transform.position;
        rb.AddForce(new Vector2(TargetDirection.x, TargetDirection.y).normalized * ShootForce, ForceMode2D.Impulse);

        float RotZ = Mathf.Atan2(-TargetDirection.y, -TargetDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, RotZ);
    }

    private void SlownessEffect() 
    {
        if (isSlownessEffected)
        {
            rb.velocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Kinematic;

            TargetAndSlownessAOEdistance = Vector3.Distance(transform.position, Target.transform.position);

            if (TargetAndSlownessAOEdistance < SlownessAOE)
            {
                Player_Script.movespeed = SlownessSpeed;
                Player_Script.GroundCheck = false;
            }
            if (TargetAndSlownessAOEdistance > SlownessAOE || Input.GetMouseButtonDown(0))
            {
                Player_Script.movespeed = PlayerOrgSpeed;
                isSlownessEffected = false;
                GameObject.Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isSlownessEffected = true;
            if (Damage > 0)
            {
                Player_Health.Damage(Damage);
                Damage = 0;
            }
        }
        else
        {
            GameObject.Destroy(gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (isSlownessEffected)
        {
            Gizmos.DrawWireCube(transform.position, new Vector3(SlownessAOE, SlownessAOE, 0));
        }
    }
}
