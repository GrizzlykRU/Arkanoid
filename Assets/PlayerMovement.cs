using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject _leftBorder;

    public GameObject _rightBorder;

    public float speed = 0.5f;

    private Vector3 position;

    public Joystick joystick;
    


    private void Awake()
    {
        position = gameObject.transform.position;
    }

    //private void FixedUpdate()
    //{
    //    if (Input.GetAxis("Horizontal") != 0)
    //    {
    //        position.x += Input.GetAxis("Horizontal") * speed;
    //    }
    //    else
    //    {
    //        position.x += Input.GetAxis("Mouse X") * mouseSensitivity* speed;
    //    }

    //    float leftBorder = _leftBorder.transform.position.x + _leftBorder.transform.localScale.x / 2,
    //          rightBorder = _rightBorder.transform.position.x - _rightBorder.transform.localScale.x / 2;

    //    if (position.x - transform.localScale.x/2 < leftBorder)
    //    {
    //        position.x = leftBorder + transform.localScale.x / 2;
    //    }
    //    else if (position.x + transform.localScale.x / 2 > rightBorder)
    //    {
    //        position.x = rightBorder - transform.localScale.x / 2;
    //    }

    //    transform.position = position;
    //}

    private void FixedUpdate()
    {
        position.x += joystick.Horizontal * speed;

        float leftBorder = _leftBorder.transform.position.x + _leftBorder.transform.localScale.x / 2,
              rightBorder = _rightBorder.transform.position.x - _rightBorder.transform.localScale.x / 2;

        if (position.x - transform.localScale.x / 2 < leftBorder)
        {
            position.x = leftBorder + transform.localScale.x / 2;
        }
        else if (position.x + transform.localScale.x / 2 > rightBorder)
        {
            position.x = rightBorder - transform.localScale.x / 2;
        }

        transform.position = position;
    }
}
