using Unity.VisualScripting;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    [SerializeField] PowerupSO powerup;
    PlayerController playerController;

    SpriteRenderer spriteRenderer;
    float timeLeft;

    void Start()
    {
        playerController = FindAnyObjectByType<PlayerController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        timeLeft = powerup.getTime();
    }

    private void Update()
    {
        CountdownTimer();
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        int layerIndex = LayerMask.NameToLayer("Player");
        if (collider2D.gameObject.layer == layerIndex
                && spriteRenderer.enabled)
        {
            spriteRenderer.enabled = false;
            playerController.ActivatePowerUp(powerup);
        }
    }

    void CountdownTimer()
    {
        if (!spriteRenderer.enabled) {
            if (timeLeft > 0)
            {
                timeLeft -=  Time.deltaTime;

                if (timeLeft <= 0)
                {
                    playerController.DeactivatePowerUp(powerup);
                }
            }
        }
    }
}
