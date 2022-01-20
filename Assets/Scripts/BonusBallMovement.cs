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
            position += direction * speed;
            transform.position = position;
            if ((DateTime.Now - startTime).Seconds >= 10)
            {
                DestroyBall();
            }
        }
    }

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _canvas = GameObject.FindGameObjectWithTag("GameField");
        direction = new Vector3(0.1f, 1.0f, 0);
        speed *= 3;
        startTime = DateTime.Now;
        position.x = _player.transform.position.x;
        position.y = _player.transform.position.y + _player.GetComponent<RectTransform>().rect.height / 2.0f / _canvas.GetComponent<RectTransform>().rect.height * Screen.height
                        + ballRadius + 1.0f;
        gameObject.transform.position = position;
        BonusBallOn bonusBall = _player.GetComponent<BonusBallOn>();
        bonusBall.bonusIsActive = true;
        isActive = true;
    }

    public override void GetCollision(List<GameObject> obstacles)
    {
        if (isActive)
        {
            foreach (var obstacle in obstacles)
            {
                if (obstacle.tag == "Obstacle")
                {
                    if (obstacle.GetComponent<ObstacleCollision>().collision)
                    {
                        continue;
                    }

                }
                if (CollisionCheck(obstacle))
                {
                    if (obstacle.name == "BottomBorder")
                    {
                        DestroyBall();
                    }

                    direction = GetReflection(obstacle);

                    if (obstacle.tag == "Obstacle")
                    {
                        ObstacleCollision collision = obstacle.GetComponent<ObstacleCollision>();
                        collision.animator.enabled = true;
                        collision.collision = true;
                        GameManager.obstacleCounter--;
                    }
                }
            }
        }

    }

    private void DestroyBall()
    {
        BonusBallOn bonusBall = _player.GetComponent<BonusBallOn>();
        bonusBall.bonusIsActive = false;
        Destroy(gameObject);
    }
}