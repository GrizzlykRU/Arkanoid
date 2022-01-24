using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevelComplete : MonoBehaviour
{
    public Animator animator;

    void FixedUpdate()
    {
        if (GameManager.levelComplete)
        {
            animator.SetBool("LevelComplete", true);
        }
    }
}
