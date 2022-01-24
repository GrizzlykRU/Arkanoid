using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BallMovement : MonoBehaviour
{
    public GameManager _gameManager;

    public GameObject _player;

    public GameObject _gameField;

    public bool isActive = false;

    public float speed = 6.0f;

    protected Vector2 velocity;

    protected Vector3 position;

    protected Rigidbody2D _rigidbody;

    public float ballRadius { get => gameObject.GetComponent<RectTransform>().rect.width / 2.0f; }

    private void Start()
    {
        position = gameObject.transform.position;
        velocity = new Vector2(0.1f, 1.0f).normalized * speed;
        _rigidbody = this.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (GameManager.gameIsActive && !isActive && (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Mouse0)))
        {
                _rigidbody.velocity = velocity;
                isActive = true;
        }
    }
    protected virtual void FixedUpdate()
    {
        if (GameManager.gameIsActive)
        {
            if (!isActive)
            {
                position.x = _player.transform.position.x;
                transform.position = position;
            }  
            else
            {
                _rigidbody.velocity = velocity;
            }
        }
        else if (GameManager.levelComplete)
        {
            _rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
    
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject obstacle = collision.gameObject;
        if (obstacle.name == "BottomBorder")
        {
            StartCoroutine(_gameManager.GetComponent<GameManager>().GameOver());
        }
        Vector3 reflection = Vector3.Reflect(velocity, collision.contacts[0].normal);
        velocity = reflection.normalized * speed;
    }
}
