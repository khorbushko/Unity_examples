using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Vector2 moveInput;
    Rigidbody2D playerRigidBody2D;
    CapsuleCollider2D playerCollider;
    [SerializeField] Animator playerAnimator;


    [SerializeField] float runSpeed = 2f;
    [SerializeField] float jumpSpeed = 10f;
    [SerializeField] float climbingSpeed = 0.24f;

    [SerializeField] BoxCollider2D feetCollider;
    [SerializeField] Vector2 deathKick = new Vector2(10f, 10f);

    float startingGravityScale = 1f;

    bool isAlive = true;

    bool isTochingGround =>
       feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"));

    bool isInClimbingArea =>
        playerCollider.IsTouchingLayers(LayerMask.GetMask("Climbing Stuff"));

    bool isTouchingEnemies =>
        playerCollider.IsTouchingLayers(LayerMask.GetMask("Enemies"));

    bool playerHasHorizontalSpeed =>
        Mathf.Abs(moveInput.x) > Mathf.Epsilon;

    bool playerHasVerticalSpeed =>
        Mathf.Abs(moveInput.y) > Mathf.Epsilon;

    void Awake()
    {
        playerRigidBody2D = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<CapsuleCollider2D>();

        startingGravityScale = playerRigidBody2D.gravityScale;
    }

    void Update()
    {
        if (isAlive)
        {
            Run();
            FlipSprite();
            ClimbLadder();
        }

        Die();
        UpdateAnimation();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (isTochingGround && value.isPressed && isAlive)
        {
            PerformJump();

        }
    }

    void PerformJump()
    {
        playerRigidBody2D.linearVelocityY += jumpSpeed;
        playerAnimator.SetBool("isJumping", true);
        Invoke("DisableJumpEffect", 0.5f);
    }

    // MARK: - Private

    private void DisableJumpEffect()
    {
        playerAnimator.SetBool("isJumping", false);
    }

    private void Run()
    {
        playerRigidBody2D.linearVelocity = new Vector2(
            moveInput.x * runSpeed,
            playerRigidBody2D.linearVelocity.y
        );
    }

    private void FlipSprite()
    {
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(
                Mathf.Sign(playerRigidBody2D.linearVelocityX),
                1f
            );
        }
    }

    private void ClimbLadder()
    {
        if (!isInClimbingArea)
        {
            playerRigidBody2D.gravityScale = startingGravityScale;
            return;
        }

        playerRigidBody2D.gravityScale = 0;

        playerRigidBody2D.linearVelocity = new Vector2(
            playerRigidBody2D.linearVelocity.x,
            moveInput.y * climbingSpeed
        );
    }

    private void UpdateAnimation()
    {
        playerAnimator.SetBool("isRunning", playerHasHorizontalSpeed);
        playerAnimator.SetBool("isClimbing", playerHasVerticalSpeed && isInClimbingArea);
    }

    private void Die()
    {
        if (isTouchingEnemies && !isAlive)
        {
            isAlive = false;
            playerAnimator.SetTrigger("Dying");

            playerRigidBody2D.linearVelocity = deathKick;
            Invoke("DisablePlayer", 0.75f);
        }
    }

    private void DisablePlayer()
    {
        playerRigidBody2D.simulated = false;
    }
}
