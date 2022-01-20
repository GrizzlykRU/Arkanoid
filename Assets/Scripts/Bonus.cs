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

    public float speed = 6.0f;

    protected Vector3 direction;

    protected Vector3 position;

    protected float ballRadius { get => gameObject.GetComponent<RectTransform>().rect.width / _canvas.GetComponent<RectTransform>().rect.width * Screen.width / 2.0f; }

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _canvas = GameObject.FindGameObjectWithTag("GameField");
        _gameManager = GameObject.FindGameObjectWithTag("GameController");
        position = transform.position;
        direction = new Vector3(0.0f, -1.0f, 0);
        speed = speed / _canvas.GetComponent<RectTransform>().rect.height * Screen.height;
    }

    private void FixedUpdate()
    {
        position += direction * speed;
        transform.position = position;
        if (CollisionCheck())
        {
            _gameManager.GetComponent<GameManager>().CreateBonusBall();
            Destroy(gameObject);
        }
    }

    private bool CollisionCheck()
    {
        float playerWidth = _player.GetComponent<RectTransform>().rect.width / _canvas.GetComponent<RectTransform>().rect.width * Screen.width;
        float playerHeight = _player.GetComponent<RectTransform>().rect.height / _canvas.GetComponent<RectTransform>().rect.height * Screen.height;
        float distanceX = Math.Abs(position.x - _player.transform.position.x);
        float distanceY = Math.Abs(position.y - _player.transform.position.y);

        if (distanceX > (playerWidth / 2.0f + ballRadius)) { return false; }
        if (distanceY > (playerHeight / 2.0f + ballRadius)) { return false; }

        return true;
    }


}
