using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public float randXStart;
    public float randXEnd;
    public float randYStart;
    public float randYEnd;
    public GameManager gm;
    private Scene scene;

    void Start()
    {
        scene = SceneManager.GetActiveScene();
        Renderer visual = GetComponent<Renderer>();
        rigidBody = GetComponent<Rigidbody2D>();
        brickReference = new Brick();
        visual.enabled = !visual.enabled;
        LaunchBall();
    }

    void Update()
    {
        if (GameManager.instance.over)
            return;
        if (scene.name == "TwoPlayerScreen")
            TwoPlayerUpdate();
        else
            RegularUpdate();
    }

    private void LaunchBall()
    {
        Renderer visual = GetComponent<Renderer>();

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) {
            float x = UnityEngine.Random.Range(0, 2) == 0 ? -1 : 1;
            Vector2 direction = new Vector2((float)UnityEngine.Random.Range(-300,300), -15f); 
            rigidBody.AddForce(direction);
            inPlay = true;
            visual.enabled = true;
        }
    }

    private void AutomaticLaunch()
    {
        Renderer visual = GetComponent<Renderer>();
        float x = UnityEngine.Random.Range(0, 2) == 0 ? -1 : 1;
        Vector2 direction = new Vector2((float)UnityEngine.Random.Range(-200, 200), -120);  // made this slightly faster, was -100
        rigidBody.AddForce(direction);
        inPlay = true;
        visual.enabled = true;
    }

    private Tuple<float, float> generateBallPosition()
    {
        if (scene.name == "TwoPlayerScreen")
		{
            randomXCoord = UnityEngine.Random.Range(randXStart, randXEnd);
            randomYCoord = UnityEngine.Random.Range(randYStart, randYEnd);
            return new Tuple<float, float>(randomXCoord, randomYCoord);
        }
        else
		{
            randomXCoord = UnityEngine.Random.Range(5f, 10f);
            randomYCoord = UnityEngine.Random.Range(3.25f, 3.75f);
            return new Tuple<float, float>(randomXCoord, randomYCoord);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // check if ball was lost
        if (other.CompareTag("Bottom"))
        {   // decrement lives
            Renderer visual = GetComponent<Renderer>();
            gm.DecrementLives();
            rigidBody.velocity = Vector2.zero;
            inPlay = false;
            visual.enabled = !visual.enabled;

            // reset brick layers for speed
            brickReference.resetLayers();
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

    void TwoPlayerUpdate()
    {
        if (!inPlay)
        {
            Tuple<float, float> ballposition = generateBallPosition();
            transform.position = new Vector3(ballposition.Item1, ballposition.Item2);
            AutomaticLaunch();
        }
        else
        {
            float yValue;
            if (rigidBody.velocity.y > -.1 && rigidBody.velocity.y < .1)
            {
                if (rigidBody.velocity.y <= 0)
                {
                    yValue = -1f;
                }
                else
                {
                    yValue = 1f;
                }
                Vector2 minimumVelocity = new Vector2(0, yValue);
                rigidBody.AddForce(minimumVelocity);
            }
        }
        previousVelocity = rigidBody.velocity;
    }

    void RegularUpdate()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && !inPlay)
        {
            Tuple<float, float> ballposition = generateBallPosition();
            transform.position = new Vector3(ballposition.Item1, ballposition.Item2);
            LaunchBall();
        }

        if (inPlay)
        {
            int yValue = -1;
            if (rigidBody.velocity.magnitude < 5)
            {
                Vector2 minimumVelocity = new Vector2(0, yValue);
                rigidBody.velocity += minimumVelocity;
            }
        }
        previousVelocity = rigidBody.velocity;
    }
}
