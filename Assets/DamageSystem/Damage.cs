using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public GameObject Enemy;
    public float DamageBar = 16;
    public Rigidbody2D EnemyRB;
    public bool Triggered;

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
        if (Triggered)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                DamageBar -= 4;
                EnemyRB.velocity = Vector2.right * 4;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Knife")
        {
            Triggered = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Knife")
        {
            Triggered = false;
        }
    }
}
