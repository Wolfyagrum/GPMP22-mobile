using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private float scoreMultiplier;

    public const string HighScoreKey = "HighScore";

    private float score;

    // Update is called once per frame
    void Update()
    {
        score += scoreMultiplier * Time.deltaTime;

        scoreText.text = Mathf.FloorToInt(score).ToString();
    }

    //when object gets destoryed check if score beat highscore if yes save it
    private void OnDestroy()
    {
        int CurrentHighScore = PlayerPrefs.GetInt(HighScoreKey, 0);
        print(CurrentHighScore);
        if (score > CurrentHighScore)
        {
            print("New highscore " + score);
            PlayerPrefs.SetInt(HighScoreKey, Mathf.FloorToInt(score));
        }
    }
}
