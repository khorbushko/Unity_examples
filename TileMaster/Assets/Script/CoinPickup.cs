using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    public AudioClip coinPickup;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            TrackCoinsPickUp();
            gameObject.SetActive(true);
            Destroy(gameObject);
        }
    }

    private void TrackCoinsPickUp()
    {
        GeneralSoundController.Instance.PlaySound(coinPickup);
        var gameSession = FindAnyObjectByType<GameSession>();
        gameSession.CollectCoin();
    }
}
