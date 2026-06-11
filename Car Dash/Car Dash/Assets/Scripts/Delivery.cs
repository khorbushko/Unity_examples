using Unity.VisualScripting;
using UnityEngine;

public class Delivery : MonoBehaviour
{
    private Driver driver;
    private bool isPackagePickedUp = false;
    [SerializeField] ParticleSystem itemParticleSyste;
    [SerializeField] ParticleSystem boostParticleSyste;

    private void Start()
    {
        driver = FindAnyObjectByType<Driver>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Package") && !isPackagePickedUp)
        {
            Debug.Log("Oww");
            isPackagePickedUp = true;
            itemParticleSyste.Play();
            Destroy(other.gameObject, 0.1f);
        }

        if (other.CompareTag("Customer") && isPackagePickedUp)
        {
            isPackagePickedUp = false;
            itemParticleSyste.Stop();
            Debug.Log("Cust");
        }

        if (other.CompareTag("Boost"))
        {
            Destroy(other.gameObject, 0.1f);
            boostParticleSyste.Play();
            driver.ApplyBoost();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        driver.RemoveBoost();
    }
}
