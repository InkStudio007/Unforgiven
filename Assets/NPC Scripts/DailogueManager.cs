using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dailoguemanager : MonoBehaviour
{
    [SerializeField]
    private bool[] DailogueBool;
    [SerializeField]
    private GameObject[] DailogueScript;
    public int SetOffDailogue;

    private void Update()
    {
        
    }
    private void DailogueSwither1()
    {
        if (DailogueBool[1])
        {
            DailogueScript[1].SetActive(false);
            DailogueScript[2].SetActive(true);
        }
    }
    private void DailogueSwither2()
    {
        if (DailogueBool[2])
        {
            DailogueScript[SetOffDailogue].SetActive(false);
            DailogueScript[3].SetActive(true);
        }
    }

}
