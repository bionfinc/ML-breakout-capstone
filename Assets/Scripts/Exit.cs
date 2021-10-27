using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Exit : MonoBehaviour
{
	public Button exitButton;

	void Start()
	{
		exitButton = GetComponent<Button>();
		exitButton.onClick.AddListener(TaskOnClick);
	}

	void TaskOnClick()
	{
		Debug.Log("Exit Button Clicked");
		Application.Quit(); // this doesn't work inside Editor
	}
}
