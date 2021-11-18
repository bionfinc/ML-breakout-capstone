using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MLPlayerButton : MonoBehaviour
{
	public Button mlPlayerButton;

	void Start()
	{
		mlPlayerButton = GetComponent<Button>();
		mlPlayerButton.onClick.AddListener(TaskOnClick);
	}

	void TaskOnClick()
	{
		SceneManager.LoadScene("MLAgentScreen");
	}
}
