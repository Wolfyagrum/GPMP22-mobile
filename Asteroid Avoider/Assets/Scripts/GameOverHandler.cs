using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverHandler : MonoBehaviour
{
    [SerializeField] private GameObject gameOverDisplay;
    [SerializeField] private AsteroidSpanwer asteroidSpanwer;
    [SerializeField] private TMP_Text gameOverText;
    [SerializeField] private ScoreHandler scoreHandler;

    public void EndGame()
    {
        asteroidSpanwer.enabled = false;

        int finalScore = scoreHandler.EndTimer();
        gameOverText.text = $"Your Score: {finalScore}";

        gameOverDisplay.SetActive(true);
    }

    public void ContuineReward()
    {
        asteroidSpanwer.enabled = true;

        scoreHandler.ContuineTimer();
        PlayerHealth ph = FindObjectOfType<PlayerHealth>();
        print(ph);
        ph.Revive();
        gameOverDisplay.SetActive(false);       
    }

    //Loads the game scene again starting up the game
    public void PlayAgain()
    {
        SceneManager.LoadScene(1);
    }

    //Load the main menu
    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
