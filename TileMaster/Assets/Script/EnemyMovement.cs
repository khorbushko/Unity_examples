using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Rigidbody2D rigidBody2D;
    [SerializeField] float moveSpeed = 1f;
    float moveDirection = 1f;
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rigidBody2D.linearVelocity = new Vector2(moveSpeed * moveDirection, 0f);
    }

    private void OnTriggerExit2D(Collider2D other)
    {        
        moveDirection *= -1f;
        FlipEnemyFacing();
    }

    void FlipEnemyFacing()
    {
        transform.localScale = new Vector2(-Mathf.Sign(rigidBody2D.linearVelocityX), 1f);
    }
}
