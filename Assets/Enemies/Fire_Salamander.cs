using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_Salamander : MonoBehaviour
{
    private Transform PlayerTransform;
    public Transform ReSpawnPoint;
    private Player_HealSystem PlayerHealth;
    public BoxCollider2D AttackArea;
    private BoxCollider2D PlayerCollider;
    private int Damage = 1;
    private bool AttackPlayer;
    private float AttackCoolDown = 0.1f;
    private float AttackTimer;
    // Start is called before the first frame update
    void Start()
    {
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        PlayerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_HealSystem>();

        AttackTimer = AttackCoolDown;
    }

    // Update is called once per frame
    void Update()
    {
        SalamanderAttack();
    }


    private void SalamanderAttack()
    {
        AttackTimer -= Time.deltaTime;
        if (AttackPlayer)
        {
            if (AttackTimer <= 0)
            {
                PlayerHealth.Damage(Damage);
                PlayerTransform.transform.position = ReSpawnPoint.transform.position;
                AttackTimer = AttackCoolDown;
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            AttackPlayer = true;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            AttackPlayer = false;
        }
    }
}
