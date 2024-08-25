using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] Dailogue;
    private int DailogueNumber;
    private bool Talking = false;
    private bool KeyPressed = false;


    private void Update()
    {
        if (Talking)
        {
            DailoguePlayer();
        }
    }
    private void DailoguePlayer()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))
        {
            Dailogue[DailogueNumber].SetActive(true);
            KeyPressed = true;
        }
        if (KeyPressed)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                DailogueNumber++;
                Dailogue[DailogueNumber - 1].SetActive(false);
                Dailogue[DailogueNumber].SetActive(true);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Talking = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Talking = false;
            DailogueNumber = 0;
            KeyPressed = false;
        }
    }

}
