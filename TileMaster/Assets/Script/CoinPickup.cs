using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    GeneralSoundController soundController;

    public AudioClip coinPickup;

    void Start()
    {
        soundController = FindAnyObjectByType<GeneralSoundController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            TrackCoinsPickUp();
            Destroy(gameObject);
        }
    }

    private void TrackCoinsPickUp()
    {
        soundController.PlaySound(coinPickup);
        var gameSession = FindAnyObjectByType<GameSession>();
        gameSession.CollectCoin();
    }
}
