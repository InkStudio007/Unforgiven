using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Health : MonoBehaviour
{
    public int EnemysHealth;

    public bool EnemyisDead = false;

    // Update is called once per frame
    void Update()
    {
        if (EnemysHealth <= 0)
        {
            EnemyisDead = true;
        }
    }

    public void TakeDamage(int Damage)
    {
        EnemysHealth -= Damage;
        Debug.Log(EnemysHealth);
    }
}
