using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knife : MonoBehaviour
{
    private float AttackTime;
    public float StartAttackTime;
    public Transform HitBoxPos;
    public float HitBoxX;
    public float HitBoxy;
    public LayerMask Enemies;
    public float Damage;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        AttackTime -= Time.deltaTime;

        if (AttackTime <= 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                AttackTime = StartAttackTime;
                Collider2D[] HitBox = Physics2D.OverlapBoxAll(HitBoxPos.position, new Vector2(HitBoxX, HitBoxy), 0, Enemies);
                for (int i = 0; i < HitBox.Length; i++)
                {
                    HitBox[i].GetComponent<Enemy>().TakeDamage(Damage);
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(HitBoxPos.position, new Vector2(HitBoxX, HitBoxy));
    }
}
