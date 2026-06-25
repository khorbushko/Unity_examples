using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    Rigidbody2D playerRigidBody2D;

    private Vector3 spawnPosition;

    void Awake()
    {
        // playerRigidBody2D = FindAnyObjectByType<PlayerMovement>();
    }

    void Start()
    {
        spawnPosition = transform.position;
    }

    private void Respawn()
    {
        transform.position = spawnPosition;
        playerRigidBody2D.linearVelocity = Vector2.zero;
        playerRigidBody2D.angularVelocity = 0f;
        playerRigidBody2D.simulated = true;

    }
}
