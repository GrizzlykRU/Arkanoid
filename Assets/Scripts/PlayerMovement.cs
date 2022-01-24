using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject _leftBorder;

    public GameObject _rightBorder;

    public Rigidbody2D _rigidbody;

    public float speed = 1.0f;

    private Vector3 position;

    private void Start()
    {
        position = gameObject.transform.position;
    }


    private void FixedUpdate()
    {
        if (GameManager.gameIsActive)
        {
            float borderWidth = _leftBorder.GetComponent<RectTransform>().rect.width;
            float width = gameObject.GetComponent<RectTransform>().rect.width;
            if (Input.GetMouseButton(0))
            {
                var pos = Input.mousePosition;
                pos.x -= Screen.width / 2;
                position = new Vector3(pos.x, transform.position.y);
            }

            float leftBorder = _leftBorder.transform.position.x + borderWidth / 2.0f,
                  rightBorder = _rightBorder.transform.position.x - borderWidth / 2.0f;

            if (position.x - width / 2.0f < leftBorder)
            {
                position.x = leftBorder + width / 2.0f;
            }
            else if (position.x + width / 2.0f > rightBorder)
            {
                position.x = rightBorder - width / 2.0f;
            }
            transform.position = position;
        }
        else if (GameManager.gameOver)
        {
            _rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else if (GameManager.levelComplete)
        {
            _rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
}
