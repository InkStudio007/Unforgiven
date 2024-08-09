using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medicine : Item
{
    private Player_HealSystem HealthSystem;
    // Start is called before the first frame update
    void Start()
    {
        HealthSystem = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_HealSystem>();
    }

    private void Update()
    {
        if (isCollided)
        {
            GameObject.Destroy(gameObject);
            HealthSystem.MedicineAmount += 1;
        }
    }
}
