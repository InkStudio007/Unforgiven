using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public BoxCollider2D DoorCollider;
    public BoxCollider2D DoorTrigger;

    private BoxCollider2D PlayerCollider;

    private bool isOpen = false;
    private bool Touching;
    // Start is called before the first frame update
    void Start()
    {
        PlayerCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Touching = DoorTrigger.IsTouching(PlayerCollider);
        if (Touching)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (isOpen == false)
                {
                    DoorCollider.isTrigger = true;
                    isOpen = true;
                }
                else
                {
                    DoorCollider.isTrigger = false;
                    isOpen = false;
                }
            }
        }

    }
}
