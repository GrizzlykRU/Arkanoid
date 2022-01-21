using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bonus : MonoBehaviour
{
    public GameObject _gameManager;

    public GameObject _player;

    public GameObject _obstacle;

    public GameObject _canvas;

    public Rigidbody2D _rigidbody;

    public float speed = 6.0f;

    protected Vector2 velocity;

    protected Vector3 position;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _canvas = GameObject.FindGameObjectWithTag("GameField");
        _gameManager = GameObject.FindGameObjectWithTag("GameController");
        position = transform.position;
        //velocity = new Vector2(0.0f, -1.0f).normalized * speed;
        speed = speed / _canvas.GetComponent<RectTransform>().rect.height * Screen.height;
        _rigidbody.velocity = new Vector2(0.0f, -1.0f).normalized * speed;

    }

    private void FixedUpdate()
    {
        
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject obstacle = collision.gameObject;
        if (obstacle.name == "BottomBorder")
        {
            Destroy(gameObject);
        }

        if (obstacle.tag == "Player")
        {
            _gameManager.GetComponent<GameManager>().CreateBonusBall();
            Destroy(gameObject);
        }
    }


}
