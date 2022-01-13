using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BallMovement : MonoBehaviour
{
    public GameObject _player;

    public GameManager _gameManager;

    public List<GameObject> obstacles;

    private int obstacleCount;

    public bool isActive = false;

    public float speed = 0.5f;

    private Vector3 direction;

    private Vector3 position;

    private void Awake()
    {
        position = gameObject.transform.position;
        direction = new Vector3(0.1f, 1.0f, 0);
    }

    private void Start()
    {
        obstacleCount = GameObject.FindGameObjectsWithTag("Obstacle").Length;
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
            GetCollision();
            position += direction * speed;
            transform.position = position;
        }
    }

    private void GetCollision()
    {

        foreach (var obstacle in obstacles)
        {
            if(obstacle.tag == "Obstacle")
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
                    _gameManager.GameOver();
                }
                
                direction = GetReflection(obstacle);

                if (obstacle.tag == "Obstacle")
                {
                    obstacle.GetComponent<ObstacleCollision>().collision = true;
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
        float distanceX = Math.Abs(position.x - obstacle.transform.position.x);
        float distanceY = Math.Abs(position.y - obstacle.transform.position.y);

        if (distanceX > (obstacle.transform.localScale.x / 2.0f + transform.localScale.x / 2.0f)) { return false; }
        if (distanceY > (obstacle.transform.localScale.y / 2.0f + transform.localScale.x / 2.0f)) { return false; }

        if (distanceX <= (obstacle.transform.localScale.x / 2.0f)) { return true; }
        if (distanceY <= (obstacle.transform.localScale.y / 2.0f)) { return true; }

        float distance = (distanceX - obstacle.transform.localScale.x / 2.0f) * (distanceX - obstacle.transform.localScale.x / 2.0f) + (distanceY - obstacle.transform.localScale.y / 2)* (distanceY - obstacle.transform.localScale.y / 2);

        return distance <= transform.localScale.x* transform.localScale.x;
    }
    
    private Vector3 GetReflection(GameObject obstacle)
    {
        Vector3 dot1;
        Vector3 dot2;
        Vector3 dot3;
        if (position.x <= obstacle.transform.position.x - obstacle.transform.localScale.x / 2 || position.x >= obstacle.transform.position.x + obstacle.transform.localScale.x / 2)
        {
            dot1 = new Vector3(obstacle.transform.position.x - obstacle.transform.localScale.x / 2, obstacle.transform.position.y - obstacle.transform.localScale.y / 2, 0);
            dot2 = new Vector3(obstacle.transform.position.x - obstacle.transform.localScale.x / 2, obstacle.transform.position.y + obstacle.transform.localScale.y / 2, 1);
            dot3 = new Vector3(obstacle.transform.position.x - obstacle.transform.localScale.x / 2, obstacle.transform.position.y, -1);
        }
        else
        {
            dot1 = new Vector3(obstacle.transform.position.x - obstacle.transform.localScale.x / 2, obstacle.transform.position.y + obstacle.transform.localScale.y / 2, 0);
            dot2 = new Vector3(obstacle.transform.position.x + obstacle.transform.localScale.x / 2, obstacle.transform.position.y + obstacle.transform.localScale.y / 2, 1);
            dot3 = new Vector3(obstacle.transform.position.x, obstacle.transform.position.y + obstacle.transform.localScale.y / 2, -1);
        }
        Vector3 side1 = dot1 - dot2;
        Vector3 side2 = dot3 - dot2;
        Vector3 normal = Vector3.Cross(side1, side2);
        return Vector3.Reflect(direction, normal.normalized);
    }
}
