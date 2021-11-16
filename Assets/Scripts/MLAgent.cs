using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

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

	// this will be passed as an observation to the ML Agent
	// 1's signify that the brick hasn't been hit, 0's mean it has
	float[,] hitBricks = new float[,] {
		{ 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f},
		{ 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f},
		{ 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f},
		{ 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f},
		{ 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f}
	};

	void Awake()
	{
		instance = this;
	}

	void Update()
	{
		if (transform.position.x != previousPaddlePosition) {
			//Debug.Log("-1 reward for moving");
			SetReward(-1f);
		}

		if (transform.position.x < leftScreenEdge)
			transform.position = new Vector3(leftScreenEdge, transform.position.y, 0);
		if (transform.position.x > rightScreenEdge)
			transform.position = new Vector3(rightScreenEdge, transform.position.y, 0);

		// check if any bricks have been broken
		if (MLGameManager.instance.bricksBroken != previousBricksBroken)
		{
			Debug.Log("+5 reward for breaking brick");
			SetReward(+5f);
			previousBricksBroken = MLGameManager.instance.bricksBroken;
		}

		// check if any lives have been lost
		if (MLGameManager.instance.lives != previousLives)
		{
			Debug.Log("-1 reward for losing life");
			SetReward(-1f);
			previousLives = MLGameManager.instance.lives;
			EndEpisode();
		}
		previousPaddlePosition = transform.position.x;
	}

	public override void CollectObservations(VectorSensor sensor)
	{
		changeInBallLocation = target.transform.localPosition - previousBallLocation;
		previousBallLocation = target.transform.localPosition;

		sensor.AddObservation(transform.localPosition);
		sensor.AddObservation(target.transform.localPosition);

		// test: add observation for distance
		sensor.AddObservation(Vector3.Distance(this.transform.localPosition, target.transform.localPosition));

		// test: add observation for ball's direction
		sensor.AddObservation(changeInBallLocation);

		// add observation for broken bricks
		for (int row = 0; row < 5; row++)
			for (int col = 0; col < 11; col++)
				sensor.AddObservation(hitBricks[row, col]);

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

		transform.position += new Vector3(moveX, 0, 0) * Time.deltaTime * moveSpeed;
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "MLBall")
		{
			Debug.Log("+1 reward for hitting ball");
			SetReward(+1f);
			//EndEpisode();
		}
	}

	public override void Heuristic(in ActionBuffers actionsOut)
	{
		ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
		continuousActions[0] = Input.GetAxisRaw("Horizontal");
	}

	public void UpdateCoords(int x, int y)
	{
		hitBricks[x, y] = 0;
	}
}
