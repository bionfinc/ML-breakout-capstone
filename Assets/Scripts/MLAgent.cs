using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class MLAgent : Agent
{
	[SerializeField] private Transform targetTransform;
	float moveSpeed = 5f;
	public float rightScreenEdge;
	public float leftScreenEdge;
	public int previousBricksBroken = 0;
	public int previousLives = 5000;

	void Update()
	{
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

	}

	public override void OnEpisodeBegin()
	{
		transform.localPosition = new Vector3(1.7853284f, 0.6854997f, 0f);
		targetTransform.localPosition = new Vector3(Random.Range(1f, 9f), Random.Range(3.25f, 3.75f), 0);
	}

	public override void CollectObservations(VectorSensor sensor)
	{
		sensor.AddObservation(transform.position);
		sensor.AddObservation(targetTransform.position);
	}

	public override void OnActionReceived(ActionBuffers actions)
	{
		float moveX = actions.ContinuousActions[0];
		transform.position += new Vector3(moveX, 0, 0) * Time.deltaTime * moveSpeed;
	}

	private void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.TryGetComponent<Ball>(out Ball ball))
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
}
