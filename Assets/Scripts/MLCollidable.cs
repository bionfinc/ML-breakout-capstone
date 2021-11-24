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
        if (coll.gameObject.tag == "MLBall")
        {
            if (!hasBeenHit)
            {
                hasBeenHit = true;

                // update player's points
                MLGameManager.instance.IncrementPoints(1);

                // update the coords for the ML Agent
                //MLAgent.instance.UpdateCoords(xPos, yPos);

                // remove the brick from the game
                Destroy(gameObject);
            }
        }
    }



}
