using UnityEngine;

public class SnowTrail : MonoBehaviour
{
    [SerializeField] ParticleSystem snowTrailParticle;
    private int floorLayerIndex;

    void Start()
    {
        floorLayerIndex = LayerMask.NameToLayer("Floor");
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.layer == floorLayerIndex)
        {
            snowTrailParticle.Play();
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.layer == floorLayerIndex)
        {
            snowTrailParticle.Stop();
        }
    }
}
