using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleLayer : MonoBehaviour
{
    private Grappling_Gun GrapplingGun;
    // Start is called before the first frame update
    void Start()
    {
        GrapplingGun = GameObject.FindGameObjectWithTag("Player").GetComponent<Grappling_Gun>();

    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GrapplingGun.GrapplingArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GrapplingGun.GrapplingArea = false;
        }
    }
}
