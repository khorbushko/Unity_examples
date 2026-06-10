using UnityEngine;

public class RotationEffect : MonoBehaviour
{
    [SerializeField] ParticleSystem rotationEffect;

    private bool facingRight = true;

    public void PlayAnimationForRotation()
    {
        rotationEffect.Play();
    }
}
