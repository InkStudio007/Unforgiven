using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medicine : MonoBehaviour
{
    private Player_HealSystem HealthSystem;
    // Start is called before the first frame update
    void Start()
    {
        HealthSystem = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_HealSystem>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            GameObject.Destroy(gameObject);
            HealthSystem.MedicineAmount += 1;
        }
    }
}
