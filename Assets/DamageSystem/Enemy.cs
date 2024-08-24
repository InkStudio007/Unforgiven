using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Health = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Health <= 0)
        {
            GameObject.DestroyImmediate(gameObject);
        }
    }

    public void TakeDamage(float Damage)
    {
        Health -= Damage;
    }
}
