using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDoor : MonoBehaviour
{
    private CannonTarget Target;

    private BoxCollider2D DoorCollider;

    private bool isOpen = false;
    // Start is called before the first frame update
    void Start()
    {
        DoorCollider = GetComponent<BoxCollider2D>();
        Target = GameObject.FindGameObjectWithTag("Cannon Target").GetComponent<CannonTarget>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Target != null)
        {
            if (Target.HitTarget)
            {
                DoorCollider.isTrigger = true;
                isOpen = true;

            }
            else
            {
                isOpen = false;
            }
        }
    }
}
