using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControlerTestVersion : MonoBehaviour
{
    public Rigidbody2D rb;
    public float up;
    public float move;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            rb.velocity = Vector2.up * up;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            rb.velocity = Vector2.right * move;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            rb.velocity = Vector2.left * move;
        }
    }
}
