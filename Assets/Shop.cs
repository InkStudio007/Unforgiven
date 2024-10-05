using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField]
    private GameObject Product;
    public int MaxProduct;

    public void Spawner()
    {
        for(int i = 0; i > MaxProduct; i++)
        {
            Instantiate(Product, transform.position, transform.rotation);
        }
    }
}
