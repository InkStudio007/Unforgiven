using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    //public KnifeCombo KnifeCombo_Script;

    private Player Player_Script;
    private Rigidbody2D Player_rb;

    public bool Hit;
    public bool Collided;
    public bool isCombo;

    public float AttackCoolDown;

    private float Vertical;

    private Vector2 Direction;
    private bool DownSlash;

    public int KnifeDamage;

    public float UpWardForce;
    public float HorizontalForce;

    private Animator KnifeAnimator;


    // Start is called before the first frame update
    void Start()
    {
        Player_Script = GameObject.FindGameObjectWithTag("Player").GetComponentInParent<Player>();
        Player_rb = GameObject.FindGameObjectWithTag("Player").GetComponentInParent<Rigidbody2D>();

        KnifeAnimator = GetComponent<Animator>();

        Hit = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vertical = Input.GetAxis("Vertical");

        AttackKnockBack();
        AttackStatus();
    }

    private void HandleAttack(Enemy_Health EnemyHealth)
    {
        if (Vertical < 0 && Player_Script.GroundCheck!)
        {
            Direction = Vector2.up;

            Collided = true;
            DownSlash = true;
        }

        if (Vertical > 0)
        {
            Direction = Vector2.down;
        }

        if (Vertical < 0 && Player_Script.GroundCheck || Vertical == 0)
        {
            if (Player_Script.isFacingRight)
            {
                Direction = Vector2.left;
            }
            else
            {
                Direction = Vector2.right;
            }
            Collided = true;
        }

        EnemyHealth.TakeDamage(KnifeDamage);

        StartCoroutine(StopAttack());
    }

    private void AttackKnockBack()
    {
        if (Collided)
        {
            if (DownSlash)
            {
                Player_rb.AddForce(Direction * UpWardForce);
            }
            else
            {
                Player_rb.AddForce(Direction * HorizontalForce);
            }
        }
    }

    private void AttackStatus()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Hit = true;
        }
        else
        {
            Hit = false;
        }

        if (Hit && Vertical > 0)
        {
            Debug.Log("UpSlash");
            KnifeAnimator.SetTrigger("UpSlash");
        }

        if (Hit && Vertical < 0 && Player_Script.GroundCheck == false)
        {
            Debug.Log("DownSlash");
            KnifeAnimator.SetTrigger("DownSlash");
        }

        if (Hit && Vertical < 0 && Player_Script.GroundCheck || Hit && Vertical == 0)
        {
            Debug.Log("ForwardSlash");
            KnifeAnimator.SetTrigger("ForwardSlash");
            //isCombo = true;
            //KnifeCombo_Script.ComboAttack();
        }
    }

    IEnumerator StopAttack()
    {
        yield return new WaitForSeconds(AttackCoolDown);

        Collided = false;
        DownSlash = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy_Health>())
        {
            Debug.Log("Collided");
            HandleAttack(collision.GetComponent<Enemy_Health>());
        }
    }
}
