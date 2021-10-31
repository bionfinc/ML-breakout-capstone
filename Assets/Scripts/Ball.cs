using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class Ball : MonoBehaviour
{ 
    public float speed = 10;
    public Rigidbody2D rigidBody;
    public Brick brickReference;
    public Vector3 previousVelocity;
    public bool inPlay;
    public float randomXCoord;
    public float randomYCoord;
    public GameManager gm;

    void Start()
    {
        Renderer visual = GetComponent<Renderer>();

        rigidBody = GetComponent<Rigidbody2D>();
        brickReference = new Brick();
        visual.enabled = !visual.enabled;
        LaunchBall();
    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && !inPlay)
        {
            Tuple<float, float> ballposition = generateBallPosition();
            transform.position = new Vector3(ballposition.Item1, ballposition.Item2);
            LaunchBall();
        }
        previousVelocity = rigidBody.velocity;
    }

    private void LaunchBall()
    {
        Renderer visual = GetComponent<Renderer>();

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) {
            inPlay = true;
            float x = UnityEngine.Random.Range(0, 2) == 0 ? -1 : 1;
            Vector2 direction = new Vector2((float)UnityEngine.Random.Range(-300,300), -300);      
            visual.enabled = true;
            rigidBody.AddForce(direction);
        }
    }

    private Tuple<float, float> generateBallPosition()
    {
        // x coordinate -4 to 4
        randomXCoord = UnityEngine.Random.Range(-4f, 4f);
        // y coordiante = -.25 to -1
        randomYCoord = UnityEngine.Random.Range(-.25f, -1f);
        return new Tuple<float, float>(randomXCoord, randomYCoord);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // check if ball was lost
        if (other.CompareTag("Bottom"))
        {   // decerement lives
            gm.DecrementLives();
            rigidBody.velocity = Vector2.zero;
            inPlay = false;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        // check if a brick was hit
        if (brickReference.colors.Contains(col.collider.tag))
        {
            int index = Array.IndexOf(brickReference.colors, col.collider.tag);
            Bounce(col.contacts[0].normal, index);
        }
        else
        {
            Bounce(col.contacts[0].normal, -1);
        }
    }

    private void Bounce(Vector3 collisionNormal, int colorIndex)
    {
        var speed = previousVelocity.magnitude;
        var direction = Vector3.Reflect(previousVelocity.normalized, collisionNormal);

        // check if not bouncing off of a brick, or if the ball has already reached the layer
        if ((colorIndex == -1) || (brickReference.layerReached[colorIndex] == true))
        {
            rigidBody.velocity = direction * speed;
        }
        else
        {
            // increase the ball's speed by the layer's index multipled by 0.5
            rigidBody.velocity = direction * (speed + (colorIndex * 0.5f));
            brickReference.layerReached[colorIndex] = true;
        }
    }

}

