using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject _leftBorder;

    public GameObject _rightBorder;

    public float speed = 1.0f;

    private Vector3 position;

    private void Awake()
    {
        position = gameObject.transform.position;
    }

    private void FixedUpdate()
    {
        float borderWidth = _leftBorder.GetComponent<RectTransform>().rect.width;
        //position.x += joystick.Horizontal * speed;
        //joystick.gameObject.transform.position = position;
        if (Input.GetMouseButton(0))
        {
            var pos = Input.mousePosition;
            position = new Vector3(pos.x, transform.position.y);
        }

        float leftBorder = _leftBorder.transform.position.x + _leftBorder.GetComponent<RectTransform>().rect.width / 2,
              rightBorder = _rightBorder.transform.position.x - _rightBorder.GetComponent<RectTransform>().rect.width / 2;

        if (position.x - gameObject.GetComponent<RectTransform>().rect.width / 2 < leftBorder)
        {
            position.x = leftBorder + gameObject.GetComponent<RectTransform>().rect.width / 2;
        }
        else if (position.x + gameObject.GetComponent<RectTransform>().rect.width / 2 > rightBorder)
        {
            position.x = rightBorder - gameObject.GetComponent<RectTransform>().rect.width / 2;
        }

        transform.position = position;
    }
}
