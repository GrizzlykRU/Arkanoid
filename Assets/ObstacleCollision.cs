using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCollision : MonoBehaviour
{
    public Animator animator;

    public bool collision = false;

    void FixedUpdate()
    {
        if (collision)
        {
            animator.SetBool("Collision", true);
        }
    }
}
