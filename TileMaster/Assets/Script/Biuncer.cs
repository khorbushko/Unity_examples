using UnityEngine;

public class Biuncer : MonoBehaviour
{
    public AudioClip bounceEffect;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            GeneralSoundController.Instance.PlaySound(bounceEffect);
        }
    }
}
