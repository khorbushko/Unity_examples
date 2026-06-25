using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    public static GameSession Instance;

    [SerializeField] int playerLives = 2;
    [SerializeField] TextMeshProUGUI scoresText;
    [SerializeField] TextMeshProUGUI livesText;

    private int coinsCollected = 0;

    public bool HasMoreLives => playerLives > 0;

    public int GetCoinsCollectedCount => coinsCollected;

    public Vector3 CurrentCheckpoint { get; set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        UpdateCanvas();
    }

    private void UpdateCanvas()
    {
        livesText.text = playerLives.ToString();
        scoresText.text = (coinsCollected * 100).ToString();
    }

    public void ProcessPlayerDeath()
    {
        if (HasMoreLives)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();
        }
    }

    void TakeLife()
    {
        playerLives -= 1;
        UpdateCanvas();
    }

    void ResetGameSession()
    {
        Destroy(gameObject);
    }

    public void RestartGame()
    {
        UpdateCanvas();
        SceneManager.LoadScene(0);
    }

    public void CollectCoin()
    {
        coinsCollected += 1;
        UpdateCanvas();
    }

    public void UpdateCheckPoint(Vector3 checkpoint)
    {
        CurrentCheckpoint = checkpoint;
    }
}
