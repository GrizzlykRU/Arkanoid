using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomTriggerEnter : MonoBehaviour
{
    public GameManager _gameManager;

    public bool triggered = false;

    void Start()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (!triggered && collider.gameObject.tag == "Ball")
        {
            triggered = true;
            StartCoroutine(_gameManager.GameOver());
        }
        else if(collider.gameObject.tag == "BonusBall")
        {
            Debug.Log("Bonus Ball Entered");
            collider.gameObject.GetComponent<BonusBallMovement>().DestroyBall();
        }
    }
}
