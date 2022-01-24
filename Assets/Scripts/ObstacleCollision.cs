using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCollision : MonoBehaviour
{
    public Animator _animator;

    private GameObject _gameManager;

    public BoxCollider2D _colider;

    public bool collisionEntered = false;

    public bool gameOver = false;

    void Awake()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameController");
    }
    void FixedUpdate()
    {
        if (collisionEntered)
        {
            _animator.SetBool("Collision", true);
        }
        if (GameManager.gameOver)
        {
            _animator.SetBool("GameOver", true);
        }
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject projectile = collision.gameObject;
        collisionEntered = true;
        _colider.enabled = false;
        GameManager.obstacleCounter--;
        _gameManager.GetComponent<GameManager>().CreateBonus(gameObject.transform.position);
    }
}
