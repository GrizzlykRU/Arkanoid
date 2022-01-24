using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BonusBallMovement : BallMovement
{
    public DateTime startTime;
    protected override void FixedUpdate()
    {
        if (isActive)
        {
            _rigidbody.velocity = velocity;
            if ((DateTime.Now - startTime).Seconds >= 10)
            {
                DestroyBall();
            }
        }
        if (GameManager.gameOver || GameManager.levelComplete)
        {
            _rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _gameField = GameObject.FindGameObjectWithTag("GameField");
        _rigidbody = this.GetComponent<Rigidbody2D>();
        speed *= 3;
        velocity = new Vector2(0.1f, 1.0f).normalized * speed;
        startTime = DateTime.Now;
        position.x = _player.transform.position.x;
        position.y = _player.transform.position.y + _player.GetComponent<RectTransform>().rect.height / 2.0f + ballRadius+1.0f;
        gameObject.transform.position = position;
        BonusBallOn bonusBall = _player.GetComponent<BonusBallOn>();
        bonusBall.bonusIsActive = true;
        isActive = true;
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        Vector3 reflection = Vector3.Reflect(velocity, collision.contacts[0].normal);
        velocity = reflection.normalized * speed;
    }

    public void DestroyBall()
    {
        BonusBallOn bonusBall = _player.GetComponent<BonusBallOn>();
        bonusBall.bonusIsActive = false;
        Destroy(gameObject);
    }
}