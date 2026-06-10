using System;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    [SerializeField] float delayForFinish = 0.5f;
    [SerializeField] ParticleSystem finishParticle;
    private PlayerController playerController;

    void Start()
    {
        playerController = FindAnyObjectByType<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        int layerIndex = LayerMask.NameToLayer("Player");
        if (other.gameObject.layer == layerIndex)
        {
            finishParticle.Play();
            Invoke(nameof(FinishGame), delayForFinish);
        }
    }

    private void FinishGame()
    {
        playerController.DisableControls(false);
        SceneManager.LoadScene("Level1");
    }
}
