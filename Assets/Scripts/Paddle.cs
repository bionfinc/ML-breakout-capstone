using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{

    public float speed = 15;
    public float rightScreenEdge;
    public float leftScreenEdge;
    Vector3 lastMousePosition;

    void Start()
    {

    }

    void WhenMouseIsMoving()
    {
        Camera cam = Camera.main;
        float height = 2f * cam.orthographicSize;
        float screenWidth = height * cam.aspect;
        float mousePosition = Input.mousePosition.x / Screen.width * screenWidth;
        Vector2 paddlePosition = new Vector2(mousePosition, transform.position.y);
        transform.position = paddlePosition;

    }

    void WhenMouseIsNotMoving()
    {
        float x = Input.GetAxisRaw("Horizontal");
        Vector3 moveDelta = new Vector3(x, 0, 0);
        transform.Translate(moveDelta.x * Time.deltaTime * speed, 0, 0);
    }

    void Update()
    {
        // for pausing
        if (Pause.active)
            return;


        if (Input.mousePosition != lastMousePosition)
        {
            lastMousePosition = Input.mousePosition;
            WhenMouseIsMoving();
        }
        else
        {
            WhenMouseIsNotMoving();
        }
        if (transform.position.x < leftScreenEdge)
            transform.position = new Vector3(leftScreenEdge, transform.position.y, 0);
        if (transform.position.x > rightScreenEdge)
            transform.position = new Vector3(rightScreenEdge, transform.position.y, 0);

    }

}
