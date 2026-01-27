using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [Header("Score Settings")]
    public int currentScore = 0;
    public TextMeshProUGUI scoreText;

    void Start()
    {
        UpdateScoreUI();
    }

    public void AddScore(int points)
    {
        currentScore += points;
        UpdateScoreUI();
        Debug.Log("Score: " + currentScore);
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + currentScore.ToString();
        }
    }

    public int GetScore()
    {
        return currentScore;
    }
}