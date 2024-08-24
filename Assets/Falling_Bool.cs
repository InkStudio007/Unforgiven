using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falling_Bool : MonoBehaviour
{
    private bool Falling = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Falling = false;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Falling = true;
    }
    private void OnTriggerStay(Collider other)
    {
        Falling = false;
    }
}
