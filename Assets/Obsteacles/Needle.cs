using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Needle : MonoBehaviour
{
    private Player_HealSystem PlayerHealth;
    public Transform ReSpawnPoint;
    private Transform PlayerTransform;

    [SerializeField] int Damage;
    // Start is called before the first frame update
    void Start()
    {
        PlayerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_HealSystem>();
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerHealth.Damage(Damage);
            PlayerTransform.position = ReSpawnPoint.position;
        }
    }
}
