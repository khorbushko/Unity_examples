using UnityEngine;
using UnityEngine.SceneManagement;

public class CrashDetector : MonoBehaviour
{
    [SerializeField] float delayForFinish = 0.5f;
    [SerializeField] ParticleSystem crashParticle;

    private PlayerController playerController;

    void Start()
    {
        playerController = FindAnyObjectByType<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        int floorLayerIndex = LayerMask.NameToLayer("Floor");
        if (other.gameObject.layer == floorLayerIndex)
        {
            playerController.DisableControls(true);
            crashParticle.Play();
            Invoke(nameof(FinishGame), delayForFinish);
        }
    }

    private void FinishGame()
    {
        playerController.DisableControls(false);
        SceneManager.LoadScene("Level1");
    }
}
