using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public GameObject Enemy;
    public float DamageBar = 16;
    public float KnifeDamage = 4;
    public float BulletDamage = 8;
    public Rigidbody2D EnemyRB;
    public bool KnifeTrigger;
    public bool BulletTrigger;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(DamageBar <= 0)
        {
            Destroy(Enemy);
        }
       if (Input.GetKeyDown(KeyCode.Mouse0) && KnifeTrigger)
       {
            DamageBar -= KnifeDamage;
            EnemyRB.velocity = Vector2.right * 4;
       }
        if (BulletTrigger)
        {
            DamageBar -= BulletDamage;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Knife")
        {
            KnifeTrigger = true;
        }
        if (collision.gameObject.tag == "Bullet")
        {
            BulletTrigger = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Knife")
        {
            KnifeTrigger = false;
        }
        if (collision.gameObject.tag == "Bullet")
        {
            BulletTrigger = false;
        }
    }
}
