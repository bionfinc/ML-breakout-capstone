using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collidable : MonoBehaviour
{
	public Rigidbody2D rigidBody;
	public bool hasBeenHit;

	void Start()
	{
		rigidBody = GetComponent<Rigidbody2D>();
		hasBeenHit = false;
	}

	void Update()
	{

	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Ball")
		{
			if (!hasBeenHit)
			{
				hasBeenHit = true;

				// update player's points
				GameManager.instance.incrementPoints();

				// remove the brick from the game
				Destroy(gameObject);
			}
		}
	}
}
