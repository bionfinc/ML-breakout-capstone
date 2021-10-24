using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Paddle paddle;
    public int score;
    public Text scoresText;
    public int lives = 5;

    private void Start()
    {
        scoresText.GetComponent<UnityEngine.UI.Text>().text = "Score: " + score;
        scoresText.text = "Score: " + score;
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

    public void incrementPoints(int changeInScore)
    {
        score += changeInScore;
        Debug.Log(("changing score", score));
        scoresText.GetComponent<UnityEngine.UI.Text>().text = "Score: " + score;
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
