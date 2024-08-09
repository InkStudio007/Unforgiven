using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootChest : MonoBehaviour
{
    private Transform PlayerTransform;
    public Transform ItemSpawnPoint;

    public List<GameObject> Items;
    public float InteractArea;
    private bool isOpen;

    // Start is called before the first frame update
    void Start()
    {
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        isOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        InteractWithChest();
    }

    private bool CanInteract()
    {
        if (Vector3.Distance(transform.position, PlayerTransform.position) < InteractArea)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void InteractWithChest()
    {
        if (CanInteract())
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (isOpen == false)
                {
                    foreach (GameObject item in Items)
                    {
                        Instantiate(item, ItemSpawnPoint);
                    }
                    Items.Clear();
                    isOpen = true;
                }
            }
        }
        else
        {
            return;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(InteractArea, InteractArea, 0));
    }
}
