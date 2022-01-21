using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCollision : MonoBehaviour
{
    public Animator animator;

    private GameObject _gameManager;

    public BoxCollider2D _colider;

    public bool collisionEntered = false;

    void Awake()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameController");
    }
    void FixedUpdate()
    {
        if (collisionEntered)
        {
            animator.SetBool("Collision", true);
        }
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject projectile = collision.gameObject;
        animator.enabled = true;
        collisionEntered = true;
        _colider.enabled = false;
        GameManager.obstacleCounter--;
        _gameManager.GetComponent<GameManager>().CreateBonus(gameObject.transform.position);
    }
}
