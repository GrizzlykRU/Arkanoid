using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BonusBallMovement : BallMovement
{
    private DateTime startTime;
    protected override void FixedUpdate()
    {
        if (isActive)
        {
            position += direction * speed;
            transform.position = position;
            Debug.Log((DateTime.Now - startTime).Seconds);
            if ((DateTime.Now - startTime).Seconds >= 10)
            {
                DestroyBall();
            }
        }
    }

    public void SetUp(GameObject player, GameObject canvas)
    {
        _player = player;
        _canvas = canvas;
        direction = new Vector3(0.1f, 1.0f, 0);
        speed *= 3;
        startTime = DateTime.Now;
        position.x = _player.transform.position.x;
        position.y = _player.transform.position.y + _player.GetComponent<RectTransform>().rect.height / 2.0f / _canvas.GetComponent<RectTransform>().rect.height * Screen.height
                        + ballRadius / _canvas.GetComponent<RectTransform>().rect.height * Screen.height;
        gameObject.transform.position = position;
        BonusBallOn bonusBall = _player.GetComponent<BonusBallOn>();
        bonusBall.bonusIsActive = true;
        isActive = true;
    }

    protected override void Start()
    {
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
                    Debug.Log("Bonus collision");
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