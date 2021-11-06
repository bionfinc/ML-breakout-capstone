using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OnePlayerButton : MonoBehaviour
{
	public Button onePlayerButton;

	void Start()
	{
		onePlayerButton = GetComponent<Button>();
		onePlayerButton.onClick.AddListener(TaskOnClick);
	}

	void TaskOnClick()
	{
		SceneManager.LoadScene("Main");
	}
}
