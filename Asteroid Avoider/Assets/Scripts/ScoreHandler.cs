using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private float scoreMultiplier;

    private bool shouldCount = true;
    private float score;

    private void Update()
    {
        if (!shouldCount)
        {
            return;
        }
        score += Time.deltaTime * scoreMultiplier;

        scoreText.text = Mathf.FloorToInt(score).ToString();
    }

    public int EndTimer()
    {
        shouldCount = false;

        scoreText.text = string.Empty;

        return Mathf.FloorToInt(score);
    }

    public void ContuineTimer()
    {
        shouldCount = true;

        scoreText.text = Mathf.FloorToInt(score).ToString();
    }
}
