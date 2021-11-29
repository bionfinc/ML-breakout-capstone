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

    }

    public void DecrementLives()
    {
        lives -= 1;
        livesText.text = "Lives: " + lives.ToString();

        // check if agent has lost all their lives
        if (lives < 1)
            PlayerDeath();
    }

    public void UpdateDisplay()
    {

    }

    public void PlayerWin()
    {
        // check if we're in 1 player or 2 player mode
        if (scene.name == "TwoPlayerScreen")
        {
            // activate the game over screen, indicating ml agent won                     
            over = true;
            destroyBallAndPaddle();
            Pause.instance.ShowEndPopUp();
        }
        else if (scene.name == "MLAgentScreen")
        {
            over = true;
            destroyBallAndPaddle();
            Pause.instance.ShowWinnerPopUp();
        }
        else
        {
            destroyBallAndPaddle();
            Pause.instance.ShowWinnerPopUp();
        }
    }

    public void PlayerDeath()
    {
        // check if we're in 1 player or 2 player mode
        if (scene.name == "TwoPlayerScreen")
        {
            over = true;
            destroyBallAndPaddle();

            // check if the other player has finished their game
            if (GameManager.instance.over)
                Pause.instance.ShowEndPopUp();
        }
        else if (scene.name == "MLAgentScreen")
		{
            over = true;
            destroyBallAndPaddle();
            Pause.instance.ShowEndPopUp();
        }
        else
        {
            DestroyCurrentGame();
            SceneManager.LoadScene("GameOverScreen");
        }

    }

    public void destroyBallAndPaddle()
	{
        GameObject paddleObject = GameObject.Find("Agent (Paddle)");
        GameObject ballObject = GameObject.Find("MLBall");
        Destroy(paddleObject);
        Destroy(ballObject);
    }

    public void DestroyCurrentGame()
    {
        Destroy(gameObject);
    }

    public void resetGame()
	{
        GameObject bricks = this.gameObject.transform.GetChild(1).gameObject;

        // go through each row of bricks and reset them
        for (int i = 0; i < 5; i++)
		{
            GameObject brickRow = bricks.gameObject.transform.GetChild(i).gameObject;

            // go through each brick in the row
            for (int j = 0; j < 11; j++)
			{
                GameObject brick = brickRow.gameObject.transform.GetChild(i).gameObject;
                brick.SetActive(true);
                brick.GetComponent<MLCollidable>().hasBeenHit = false;

            }

		}

    }

}
