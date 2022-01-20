using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusBallOn : MonoBehaviour
{
    public Animator animator;

    public bool bonusIsActive = false;

    void FixedUpdate()
    {
        if (bonusIsActive)
        {
            animator.SetBool("BonusBall", true);
        }
        else
        {
            animator.SetBool("BonusBall", false);
        }
    }
}
