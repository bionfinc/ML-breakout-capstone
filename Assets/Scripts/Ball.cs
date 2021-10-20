using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

	public float speed;
	public Rigidbody2D rigidBody;

	void Start()
	{
		rigidBody = GetComponent<Rigidbody2D>();
		LaunchBall();
	}

	void Update()
	{

	}

	private void LaunchBall()
	{
		float x = Random.Range(0, 2) == 0 ? -1 : 1;
		rigidBody.velocity = new Vector2(1 * speed, -1 * speed);
	}


	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Bottom")
		{
			// decerement lives
			// reset episode
		}
	}
}

