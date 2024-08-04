using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movable_Box : MonoBehaviour
{
    private Player Player;
    private Rigidbody2D rb;
    public float MoveSpeed = 1f;
    private bool isBox;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isBox)
        {
            if (Player.Horizontal > 0)
            {
                rb.MovePosition(new Vector3(rb.position.x + MoveSpeed, rb.position.y, 0));
            }
            if (Player.Horizontal < 0)
            {
                rb.MovePosition(new Vector3(rb.position.x - MoveSpeed, rb.position.y, 0));
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isBox = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isBox = false;
        }
    }
}
