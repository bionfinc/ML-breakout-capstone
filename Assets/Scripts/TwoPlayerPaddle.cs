using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoPlayerPaddle : MonoBehaviour
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
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 paddlePosition = new Vector3(mousePosition.x, transform.position.y, 0);
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
        if (transform.localPosition.x < leftScreenEdge)
            transform.localPosition = new Vector3(leftScreenEdge, 0.9f, 0);
        if (transform.localPosition.x > rightScreenEdge)
            transform.localPosition = new Vector3(rightScreenEdge, 0.9f, 0);
    }

}
