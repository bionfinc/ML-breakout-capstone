using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MLCollidable : MonoBehaviour
{
    public bool hasBeenHit;
    public int xPos;
    public int yPos;

    void Start()
    {
        hasBeenHit = false;
    }

    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "MLBall")                            // changed this for testing
        {
            if (!hasBeenHit)
            {
                hasBeenHit = true;

                // update player's points
                MLGameManager.instance.IncrementPoints(1);

                // remove the brick from the game
                Destroy(gameObject);
            }
        }
    }



}
