using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Camera MainCam;
    private Rigidbody2D rb;
    private Vector3 MousePos;
    public float Force;
    // Start is called before the first frame update
    void Start()
    {
        MainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rb = gameObject.GetComponent<Rigidbody2D>();

        MousePos = MainCam.ScreenToWorldPoint(Input.mousePosition);

        Vector3 Direction = MousePos - transform.position;
        Vector3 Rotation = transform.position - MousePos;

        rb.velocity = new Vector2(Direction.x, Direction.y).normalized * Force;

        float RotZ = Mathf.Atan2(Rotation.y, Rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, RotZ + 90);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            GameObject.Destroy(gameObject);
        }
    }
}
