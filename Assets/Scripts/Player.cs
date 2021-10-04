using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	private BoxCollider2D boxCollider;
	private RaycastHit2D hit;
	private Vector3 moveDelta;

	private void Start()
	{
		boxCollider = GetComponent<BoxCollider2D>();
	}

	private void FixedUpdate()
	{
		// look for inputs on keyboard, add them up to moveDelta
		float x = Input.GetAxisRaw("Horizontal");

		// reset moveDelta
		moveDelta = new Vector3(x, 0, 0);

		//make sure we can move along the x axis
		hit = Physics2D.BoxCast(origin: transform.position,
								size: boxCollider.size,
								angle: 0,
								direction: new Vector2(moveDelta.x, 0),
								distance: Mathf.Abs(moveDelta.x * Time.deltaTime) * 14,  
								layerMask: LayerMask.GetMask("Actor", "Blocking"));

		if (hit.collider == null)
			transform.Translate(moveDelta.x * Time.deltaTime * 14, 0, 0);
	}

}
