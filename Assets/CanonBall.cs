using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonBall : MonoBehaviour
{
    private Player_HealSystem Player_Health;
    public int Damage;
    // Start is called before the first frame update
    void Start()
    {
        Player_Health = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_HealSystem>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player_Health.Damage(Damage);
        }
        GameObject.Destroy(gameObject);
    }
}
