using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MLGameManager : MonoBehaviour
{
    public static MLGameManager instance;
    public Paddle paddle;
    public int score;
    public Text scoresText;
    public Text livesText;
    public int lives;
    public int bricksBroken = 0;


    private void Start()
    {
        lives = 100000;
        scoresText.text = "Score: " + score.ToString();
        livesText.text = "Lives: " + lives.ToString();

    }

    private void Awake()
    {
        if (MLGameManager.instance != null)
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
        scoresText.text = "Score: " + score.ToString();
        bricksBroken++;

        // check if agent has won the game
        if (score == 55)
            PlayerWin();
        // call next level or new scene
    }

    public void DecrementLives()
    {
        lives -= 1;
        livesText.text = "Lives: " + lives.ToString();

        // check if agent has lost all their lives
        if (lives < 1)
            PlayerDeath();
        // call new scene
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
