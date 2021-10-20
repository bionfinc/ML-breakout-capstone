using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;
	public Paddle paddle;
	public int points = 0;
	public int lives = 5;

	private void Start()
	{

	}

    private void Awake()
	{
		if (GameManager.instance != null)
		{
			Destroy(gameObject);
			return;
		}

		// make sure there's only one instance of the GameManager
		instance = this;
		DontDestroyOnLoad(gameObject);
	}

	public void incrementPoints()
	{

	}

	public void decrementLives()
	{

	}

	public void updateDisplay()
	{

	}

	public void playerDeath()
	{

	}
    
}
