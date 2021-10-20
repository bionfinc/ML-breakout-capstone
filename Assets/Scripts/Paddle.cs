using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour {

	public float speed = 15;
	public float rightScreenEdge;
	public float leftScreenEdge;

	void Start()
	{

	}

	void Update()
	{
		float x = Input.GetAxisRaw("Horizontal");
		Vector3 moveDelta = new Vector3(x, 0, 0);

		transform.Translate(moveDelta.x * Time.deltaTime * speed, 0, 0);
		if (transform.position.x < leftScreenEdge)
			transform.position = new Vector3(leftScreenEdge, transform.position.y, 0);
		if (transform.position.x > rightScreenEdge)
			transform.position = new Vector3(rightScreenEdge, transform.position.y, 0);
	}

}
