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
            //position += velocity * speed;
            _rigidbody.velocity = velocity;
            //transform.position = position;
            if ((DateTime.Now - startTime).Seconds >= 10)
            {
                DestroyBall();
            }
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
        position.y = _player.transform.position.y + _player.GetComponent<RectTransform>().rect.height / 2.0f/*/ _gameField.GetComponent<RectTransform>().rect.height * Screen.height*/ + ballRadius+1.0f;
        gameObject.transform.position = position;
        BonusBallOn bonusBall = _player.GetComponent<BonusBallOn>();
        bonusBall.bonusIsActive = true;
        isActive = true;
    }

    //public override void GetCollision(List<GameObject> obstacles)
    //{
    //    if (isActive)
    //    {
    //        foreach (var obstacle in obstacles)
    //        {
    //            if (obstacle.tag == "Obstacle")
    //            {
    //                if (obstacle.GetComponent<ObstacleCollision>().collision)
    //                {
    //                    continue;
    //                }

    //            }
    //            if (CollisionCheck(obstacle))
    //            {
    //                if (obstacle.name == "BottomBorder")
    //                {
    //                    DestroyBall();
    //                }

    //                velocity = GetReflection(obstacle);

    //                if (obstacle.tag == "Obstacle")
    //                {
    //                    ObstacleCollision collision = obstacle.GetComponent<ObstacleCollision>();
    //                    collision.animator.enabled = true;
    //                    collision.collision = true;
    //                    GameManager.obstacleCounter--;
    //                }
    //            }
    //        }
    //    }

    //}

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject obstacle = collision.gameObject;
        if (obstacle.name == "BottomBorder")
        {
            DestroyBall();
        }
        //velocity = GetReflection(obstacle);
        Vector3 reflection = Vector3.Reflect(velocity, collision.contacts[0].normal);
        velocity = reflection.normalized * speed;
    }

    private void DestroyBall()
    {
        BonusBallOn bonusBall = _player.GetComponent<BonusBallOn>();
        bonusBall.bonusIsActive = false;
        Destroy(gameObject);
    }
}