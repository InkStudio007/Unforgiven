using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public GameObject Gun;
    public GameObject Knife;
    private bool isKnife = true;
    // Start is called before the first frame update
    void Start()
    {
        Gun.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        WeaponSwitch();
    }

    void WeaponSwitch()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (isKnife)
            {
                Gun.SetActive(true);
                Knife.SetActive(false);
                isKnife = false;
            }
            else
            {
                Gun.SetActive(false);
                Knife.SetActive(true);
                isKnife = true;
            }
            
        }
    }
}
