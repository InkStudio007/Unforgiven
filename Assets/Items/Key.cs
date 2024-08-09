using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Item
{
    private Inventory inventory;

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (isCollided)
        {
            inventory.KeyCount += 1;
            GameObject.Destroy(gameObject);
        }
    }
}
