using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonTarget : MonoBehaviour
{
    public bool HitTarget = false;
    // Start is called before the first frame update
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "CannonBall")
        {
            HitTarget = true;
        }
    }
}
