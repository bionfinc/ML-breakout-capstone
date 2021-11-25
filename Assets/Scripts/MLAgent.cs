using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine.SceneManagement;

public class MLAgent : Agent
{
	public static MLAgent instance;
	public GameObject target;
	float moveSpeed = 5f;
	public float rightScreenEdge;
	public float leftScreenEdge;
	public int previousBricksBroken = 0;
	public int previousLives = 5000;
	Vector3 previousBallLocation;
	Vector3 changeInBallLocation;
	float previousPaddlePosition;
	private Scene scene;


	void Awake()
	{
		instance = this;
		scene = SceneManager.GetActiveScene();
	}

	void Update()
	{
		/*
		if (transform.position.x != previousPaddlePosition) {            // commented this out for testing
			//Debug.Log("-1 reward for moving");
			AddReward(-0.001f);
		}
		*/

		if (transform.localPosition.x < leftScreenEdge)
			transform.localPosition = new Vector3(leftScreenEdge, transform.localPosition.y, 0);
		if (transform.localPosition.x > rightScreenEdge)
			transform.localPosition = new Vector3(rightScreenEdge, transform.localPosition.y, 0);

		// check if any bricks have been broken
		if (MLGameManager.instance.bricksBroken != previousBricksBroken)
		{
			AddReward(+1f);
			previousBricksBroken = MLGameManager.instance.bricksBroken;
		}

		// check if any lives have been lost
		if (MLGameManager.instance.lives != previousLives)
		{
			AddReward(-1f);
			previousLives = MLGameManager.instance.lives;
			//EndEpisode();
		}
		previousPaddlePosition = transform.position.x;
	}

	public override void OnEpisodeBegin()
	{
		if (scene.name == "TwoPlayerScreen")
		{
			transform.localPosition = new Vector3(4f, 0.6854997f, 0f);
			target.transform.localPosition = new Vector3(Random.Range(-9f, -5f), Random.Range(0.2f, 1f), 0);
		}
		else
		{
			transform.localPosition = new Vector3(4f, 0.6854997f, 0f);
			target.transform.localPosition = new Vector3(Random.Range(1f, 9f), Random.Range(3.25f, 3.75f), 0);
		}
	}

	public override void CollectObservations(VectorSensor sensor)
	{
		changeInBallLocation = target.transform.localPosition - previousBallLocation;
		previousBallLocation = target.transform.localPosition;

		sensor.AddObservation(transform.localPosition);
		sensor.AddObservation(target.transform.localPosition);

		// add observation for distance
		sensor.AddObservation(Vector3.Distance(this.transform.localPosition, target.transform.localPosition));

		// add observation for ball's direction
		sensor.AddObservation(changeInBallLocation);

		// add observation for paddle distance to screen edges
		sensor.AddObservation(transform.localPosition.x - leftScreenEdge);
		sensor.AddObservation(rightScreenEdge - transform.localPosition.x);
	}

	public override void OnActionReceived(ActionBuffers actions)
	{
		// here we're getting which one of the 3 actions was sent to the action buffer
		// then changing the x position depending on what the action was
		int moveX = 0;
		int action = actions.DiscreteActions[0];

		switch (action)
		{
			case 0:
				moveX = -1;
				break;
			case 1:
				moveX = 0;
				break;
			case 2:
				moveX = 1;
				break;
		}

		transform.localPosition += new Vector3(moveX, 0, 0) * Time.deltaTime * moveSpeed;
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "MLBall")
			AddReward(+0.5f);
	}
}
