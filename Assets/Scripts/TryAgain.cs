using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TryAgain : MonoBehaviour
{
	public void LoadGame()
	{
		SceneManager.LoadScene("Main");
	}
}
