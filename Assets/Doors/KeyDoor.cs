using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoor : MonoBehaviour
{
    private Inventory inventory;

    public BoxCollider2D DoorCollider;
    public BoxCollider2D DoorTrigger;

    private BoxCollider2D PlayerCollider;

    private bool isOpen = false;
    private bool Touching;
    // Start is called before the first frame update
    void Start()
    {
        PlayerCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider2D>();
        inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        Touching = DoorTrigger.IsTouching(PlayerCollider);
        if (Touching)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (inventory.KeyCount > 0)
                {
                    if (isOpen == false)
                    {
                        DoorCollider.isTrigger = true;
                        isOpen = true;
                        inventory.KeyCount -= 1;
                    }
                }
            }
        }

    }
}

