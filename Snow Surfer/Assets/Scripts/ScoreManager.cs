using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int score = 0;
    [SerializeField] TextMeshProUGUI scoreText;

    private void Start() {
        UpdateScore();
    }
    public void AddScore(int scores)
    {
        score += scores;
        UpdateScore();
    }

    private void UpdateScore()
    {
        scoreText.text = "Score: " + score.ToString();
    }
}
