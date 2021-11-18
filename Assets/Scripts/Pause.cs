using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
	public static Pause instance;
	public static bool active = false;
	public GameObject menu;
	private Scene scene;
	private GameObject gameOverHeader;
	private GameObject winnerHeader;

	void Start()
	{
		scene = SceneManager.GetActiveScene();
		instance = this;
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (active)
				ResumeGame();
			else
				PauseGame();
		}
	}

	public void ResumeGame()
	{
		menu.SetActive(false);
		Time.timeScale = 1f;
		active = false;
	}

	public void PauseGame()
	{
		if (scene.name == "Main")
		{
			menu.SetActive(true);
			Time.timeScale = 0f;
			active = true;
		}
		else
		{ // this is for ml agent mode and two player mode
			menu.SetActive(true);

			// make it so neither game over header appears during a normal pause
			gameOverHeader = this.gameObject.transform.GetChild(0).GetChild(2).gameObject;
			gameOverHeader.SetActive(false);
			winnerHeader = this.gameObject.transform.GetChild(0).GetChild(3).gameObject;
			winnerHeader.SetActive(false);

			Time.timeScale = 0f;
			active = true;
		}

	}

	public void ExitGame()
	{
		Debug.Log("Exit Game pressed");
	}

	public void ShowEndPopUp()
	{
		menu.SetActive(true);

		// make it so only red game over header shows
		winnerHeader = this.gameObject.transform.GetChild(0).GetChild(3).gameObject;
		winnerHeader.SetActive(false);

		Time.timeScale = 0f;
		active = true;
	}

	public void ShowWinnerPopUp()
	{
		menu.SetActive(true);

		// make it so only green game over header shows
		gameOverHeader = this.gameObject.transform.GetChild(0).GetChild(2).gameObject;
		gameOverHeader.SetActive(false);

		Time.timeScale = 0f;
		active = true;
	}

	public void LoadMain()
	{
		if (scene.name == "TwoPlayerScreen")
		{
			ResumeGame();
			GameManager.instance.DestroyCurrentGame();
			MLGameManager.instance.DestroyCurrentGame();
			SceneManager.LoadScene("OpeningMenu");
		}
		else if (scene.name == "Main")
		{
			ResumeGame();
			GameManager.instance.DestroyCurrentGame();
			SceneManager.LoadScene("OpeningMenu");
		}
		else if (scene.name == "MLAgentScreen")
		{
			ResumeGame();
			MLGameManager.instance.DestroyCurrentGame();
			SceneManager.LoadScene("OpeningMenu");
		}

	}



}
