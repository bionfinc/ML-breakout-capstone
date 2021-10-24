using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class Ball : MonoBehaviour {

	public float speed = 5;
	public Rigidbody2D rigidBody;
	public Brick brickReference;
	public Vector3 previousVelocity;
	bool hasStarted = false;

	void Start()
	{
		rigidBody = GetComponent<Rigidbody2D>();
		brickReference = new Brick();
	}

	void Update()
	{
		if (hasStarted == false) {
			LaunchBall();
		}
		previousVelocity = rigidBody.velocity;
	}

	private void LaunchBall()
	{
		if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) {
			float x = UnityEngine.Random.Range(0, 2) == 0 ? -1 : 1;
			rigidBody.velocity = new Vector2(1 * speed, -1 * speed);
		}
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		// check if ball was lost
		if (col.gameObject.tag == "Bottom")
		{	// decerement lives and reset episode
		}

		// check if a brick was hit
		if (brickReference.colors.Contains(col.collider.tag))
		{
			int index = Array.IndexOf(brickReference.colors, col.collider.tag);
			Bounce(col.contacts[0].normal, index);
		}
		else 
		{
			Bounce(col.contacts[0].normal, - 1);
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

