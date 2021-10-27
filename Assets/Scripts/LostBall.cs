using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LostBall : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        GameManager.instance.DecrementLives();
        SceneManager.LoadScene("Main");
    }
}
