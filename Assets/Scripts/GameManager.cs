using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Paddle paddle;
    public int score;
    public UnityEngine.UI.Text scoresText;
    public UnityEngine.UI.Text livesText;
    public int lives;


    private void Start()
    {
        lives = 3;
        scoresText = GameObject.FindGameObjectWithTag("ScoresText").GetComponent<UnityEngine.UI.Text>();
        scoresText.text = "Score: " + score.ToString();
        livesText = GameObject.FindGameObjectWithTag("LivesText").GetComponent<UnityEngine.UI.Text>();
        livesText.text = "Lives: " + lives.ToString();

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

    public void IncrementPoints(int changeInScore)
    {
        score += changeInScore;
        scoresText.GetComponent<UnityEngine.UI.Text>().text = "Score: " + score.ToString();

        // check if player has won the game
        if (score == 55)
            PlayerWin();
    }

    public void DecrementLives()
    {
        lives -= 1;
        livesText.GetComponent<UnityEngine.UI.Text>().text = "Lives: " + lives.ToString();

        // check if player has lost all their lives
        if (lives < 1)
            PlayerDeath();
    }

    public void UpdateDisplay()
    {

    }

    public void PlayerWin()
    {
        DestroyCurrentGame();
        // change scene to win scene
        // SceneManager.LoadScene("WinScene");
    }

    public void PlayerDeath()
    {
        DestroyCurrentGame();
        // change scene to lose scene
        SceneManager.LoadScene("GameOverScreen");
    }

    public void DestroyCurrentGame()
	{
        Destroy(gameObject);
    }

}
