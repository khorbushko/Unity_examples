using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession: MonoBehaviour
{
    [SerializeField] int playerLives = 2;
    GeneralSoundController soundController;

    private int coinsCollected = 0;

    public bool HasMoreLives => playerLives > 0;

    public int GetCoinsCollectedCount => coinsCollected;

    public AudioClip levelBackgroundAudioClip;
    public AudioClip gameOverAudioClip;

    private void Awake()
    {
        int numberOfGameSessions = FindObjectsByType<GameSession>().Length;
        if (numberOfGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

    }

    void Start()
    {
        soundController = FindAnyObjectByType<GeneralSoundController>();
        soundController.PlaySoundLoop(levelBackgroundAudioClip);
    }

    public void ProcessPlayerDeath()
    {
        if (HasMoreLives)
        {
            TakeLife();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            ResetGameSession();
        }
    }

    void TakeLife()
    {
        playerLives -= 1;
    }

    void ResetGameSession()
    {
        soundController.StopLoopSound();
        soundController.PlaySound(gameOverAudioClip);

        // SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    public void CollectCoin()
    {
        coinsCollected += 1;
    }
}
