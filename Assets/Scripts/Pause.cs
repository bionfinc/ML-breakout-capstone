using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
	public static bool active = false;
	public GameObject menu;

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
		menu.SetActive(true);
		Time.timeScale = 0f;
		active = true;
	}

	public void ExitGame()
	{
		Debug.Log("Exit Game pressed");
	}

	public void LoadMain()
	{
		ResumeGame();
		GameManager.instance.DestroyCurrentGame();
		SceneManager.LoadScene("OpeningMenu");
	}



}
