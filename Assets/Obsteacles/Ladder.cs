using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    private float Vertical;
    public float MoveSpeed;
    public bool isLadder;
    public bool isClimbing;

    private Rigidbody2D PlayerRb;
    // Start is called before the first frame update
    void Start()
    {
        PlayerRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vertical = Input.GetAxisRaw("Vertical");

        if (isLadder && Vertical > 0f)
        {
            isClimbing = true;
        }
    }

    public void ClibeLadder()
    {
        if (isClimbing)
        {
            PlayerRb.gravityScale = 0f;
            PlayerRb.velocity = new Vector2(PlayerRb.velocity.x, Vertical * MoveSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            isLadder = false;
            isClimbing = false;
        }
    }
}
