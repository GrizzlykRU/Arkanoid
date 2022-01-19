using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BallMovement : MonoBehaviour
{
    public GameObject _player;

    public GameObject _canvas;

    public int obstacleCount = 0;

    public bool isActive = false;

    public float speed = 1.0f;

    private Vector3 direction;

    private Vector3 position;

    private float ballRadius { get => gameObject.GetComponent<RectTransform>().rect.width / _canvas.GetComponent<RectTransform>().rect.width * Screen.width /2.0f; }

    private void Start()
    {
        position = gameObject.transform.position;
        direction = new Vector3(0.1f, 1.0f, 0);
        speed = speed / _canvas.GetComponent<RectTransform>().rect.height * Screen.height;
    }
    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Mouse0))
        {
            if (!isActive)
            {
                position = gameObject.transform.position;
                position += direction * speed;
                transform.position = position;
                isActive = true;
            }
        }

        if (!isActive)
        {
            position.x = _player.transform.position.x;
            transform.position = position;
        }  
        else
        {
            position += direction * speed;
            transform.position = position;
        }
    }

    public void GetCollision(List<GameObject> obstacles)
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
                    FindObjectOfType<GameManager>().GameOver();
                }
                
                direction = GetReflection(obstacle);

                if (obstacle.tag == "Obstacle")
                {
                    ObstacleCollision collision = obstacle.GetComponent<ObstacleCollision>();
                    collision.animator.enabled = true;
                    collision.collision = true;
                    obstacleCount--;
                    if (obstacleCount == 0)
                    {
                        enabled = false;
                        FindObjectOfType<GameManager>().LevelComplete();
                    }
                }
            }
    }
    }

    private bool CollisionCheck(GameObject obstacle)
    {
        float obstacleWidth = obstacle.GetComponent<RectTransform>().rect.width / _canvas.GetComponent<RectTransform>().rect.width * Screen.width;
        float obstacleHeight = obstacle.GetComponent<RectTransform>().rect.height / _canvas.GetComponent<RectTransform>().rect.height * Screen.height;
        float distanceX = Math.Abs(position.x - obstacle.transform.position.x);
        float distanceY = Math.Abs(position.y - obstacle.transform.position.y);

        if (distanceX > (obstacleWidth / 2.0f + ballRadius)) { return false; }
        if (distanceY > (obstacleHeight / 2.0f + ballRadius)) { return false; }

        return true;

        //if (distanceX <= (obstacleWidth / 2.0f)) { return true; }
        //if (distanceY <= (obstacleHeight / 2.0f)) { return true; }

        //float distance = (distanceX - obstacleWidth / 2.0f) * (distanceX - obstacleWidth / 2.0f) + (distanceY - obstacleHeight / 2.0f) * (distanceY - obstacleHeight / 2.0f);

        //return distance <= ballRadius * ballRadius;
    }
    
    private Vector3 GetReflection(GameObject obstacle)
    {
        Vector3 dot1;
        Vector3 dot2;
        Vector3 dot3;

        float obstacleWidth = obstacle.GetComponent<RectTransform>().rect.width / _canvas.GetComponent<RectTransform>().rect.width * Screen.width;
        float obstacleHeight = obstacle.GetComponent<RectTransform>().rect.height / _canvas.GetComponent<RectTransform>().rect.height * Screen.height;

        if (position.x <= obstacle.transform.position.x - obstacleWidth / 2.0f || position.x >= obstacle.transform.position.x + obstacleWidth / 2.0f)
        {
            dot1 = new Vector3(obstacle.transform.position.x - obstacleWidth / 2.0f, obstacle.transform.position.y - obstacleHeight / 2.0f, 0);
            dot2 = new Vector3(obstacle.transform.position.x - obstacleWidth / 2.0f, obstacle.transform.position.y + obstacleHeight / 2.0f, 1);
            dot3 = new Vector3(obstacle.transform.position.x - obstacleWidth / 2.0f, obstacle.transform.position.y, -1);
        }
        else
        {
            dot1 = new Vector3(obstacle.transform.position.x - obstacleWidth / 2.0f, obstacle.transform.position.y + obstacleHeight / 2.0f, 0);
            dot2 = new Vector3(obstacle.transform.position.x + obstacleWidth / 2.0f, obstacle.transform.position.y + obstacleHeight / 2.0f, 1);
            dot3 = new Vector3(obstacle.transform.position.x, obstacle.transform.position.y + obstacleHeight / 2.0f, -1);
        }
        Vector3 side1 = dot1 - dot2;
        Vector3 side2 = dot3 - dot2;
        Vector3 normal = Vector3.Cross(side1, side2);
        return Vector3.Reflect(direction, normal.normalized);
    }
}
