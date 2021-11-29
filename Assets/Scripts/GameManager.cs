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
    public Text scoresText;
    public Text livesText;
    public int lives;
    Scene scene;
    public bool over;


    private void Start()
    {
        lives = 5;
        scoresText.text = "Score: " + score.ToString();
        livesText.text = "Lives: " + lives.ToString();
        scene = SceneManager.GetActiveScene();
        over = false;
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
        scoresText.text = "Score: " + score.ToString();

        // check if player has won the game
        if (score == 55)
            PlayerWin();
            // call next level or new scene
    }

    public void DecrementLives()
    {
        lives -= 1;
        livesText.text = "Lives: " + lives.ToString();

        // check if player has lost all their lives
        if (lives < 1)
            PlayerDeath();
            // call new scene
    }

    public void UpdateDisplay()
    {

    }

    public void PlayerWin()
    {
        // check if we're in 1 player or 2 player mode
        if (scene.name == "TwoPlayerScreen")
        {
            over = true;
            GameObject paddleObject = GameObject.Find("Paddle");
            Destroy(paddleObject);
            Pause.instance.ShowWinnerPopUp();
        }
        else
        {
            DestroyCurrentGame();
            SceneManager.LoadScene("WinScreen");
        }
    }

    public void PlayerDeath()
    {
        // check if we're in 1 player or 2 player mode
        if (scene.name == "TwoPlayerScreen")
        {
            over = true;
            GameObject paddleObject = GameObject.Find("Paddle");
            Destroy(paddleObject);

            // check if the other player has finished their game
            if (MLGameManager.instance.over)
            {
                Pause.instance.ShowEndPopUp();
            }
        }
        else
        {
            DestroyCurrentGame();
            SceneManager.LoadScene("GameOverScreen");
        }

    }

    public void DestroyCurrentGame()
    {
        Destroy(gameObject);
    }

}
