using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject _leftBorder;

    public GameObject _canvas;

    public GameObject _rightBorder;

    public float speed = 1.0f;

    private Vector3 position;

    private void Start()
    {
        position = gameObject.transform.position;
    }

    private void FixedUpdate()
    {
        float borderWidth = _leftBorder.GetComponent<RectTransform>().rect.width / _canvas.GetComponent<RectTransform>().rect.width * Screen.width;
        float width = gameObject.GetComponent<RectTransform>().rect.width / _canvas.GetComponent<RectTransform>().rect.width * Screen.width;
        if (Input.GetMouseButton(0))
        {
            var pos = Input.mousePosition;
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

    private void OnCollisionEnter2D(Collision collision)
    {
        GameObject obstacle = collision.gameObject;
        if(obstacle.tag == "Border")
        {

        }
    }
}
