/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeCombo : MonoBehaviour
{
    public IdleStateBehaviour IdleBeahaviour;
    public Knife Knife_Script;
    public Animator ComboAnimator;
    // Start is called before the first frame update
    void Start()
    {
        ComboAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    public void ComboAttack()
    {
        if (IdleBeahaviour.isIdle == false)
        {
            if (Knife_Script.isCombo)
            {
                if (Knife_Script.Collided)
                {
                    ComboAnimator.SetTrigger("Combo Attack Two");

                    if (Knife_Script.Collided)
                    {
                        ComboAnimator.SetTrigger("Combo Attack Three");
                        Knife_Script.isCombo = false;
                    }
                    else
                    {
                        Knife_Script.isCombo = false;
                    }
                }
                else
                {
                    Knife_Script.isCombo = false;
                }
            }
        }
        else
        {
            Knife_Script.isCombo = false;
        }
    }
}
*/
