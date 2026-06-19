using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    Vector2 moveInput;
    Rigidbody2D playerRigidBody2D;
    CapsuleCollider2D playerCollider;
    [SerializeField] Animator playerAnimator;

    [SerializeField] CinemachineImpulseSource impulseSource;
    [SerializeField] CinemachineCamera cinemachineCamera;

    [SerializeField] GameObject arrow;
    [SerializeField] Transform bow;


    [SerializeField] float runSpeed = 2f;
    [SerializeField] float jumpSpeed = 10f;
    [SerializeField] float climbingSpeed = 0.24f;

    [SerializeField] float swimSpeed = 1.5f;
    [SerializeField] float swimGravityScale = 2f;
    [SerializeField] float swimUpSpeed = 2f;

    [SerializeField] BoxCollider2D feetCollider;
    [SerializeField] Vector2 deathKick = new Vector2(10f, 10f);
    [SerializeField] Image deathOverlay;

    float startingGravityScale = 1f;

    bool isAlive = true;

    bool isTochingGround =>
       feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"));

    bool isInClimbingArea =>
        playerCollider.IsTouchingLayers(LayerMask.GetMask("Climbing Stuff"));

    bool isTouchingEnemies =>
        playerCollider.IsTouchingLayers(LayerMask.GetMask("Enemies"));

    bool isTouchingHazards =>
        playerCollider.IsTouchingLayers(LayerMask.GetMask("Hazards")) ||
        feetCollider.IsTouchingLayers(LayerMask.GetMask("Hazards"));

    bool isInWater =>
        playerCollider.IsTouchingLayers(LayerMask.GetMask("Water"));

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
            Swim();

            if (!isInWater)
            {
                Run();
                ClimbLadder();
            }

            FlipSprite();
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
        if (!value.isPressed || !isAlive) return;

        if (isInWater)
        {
            playerRigidBody2D.linearVelocity = new Vector2(
                playerRigidBody2D.linearVelocity.x,
                jumpSpeed * 0.4f
            );
            return;
        }

        if (isTochingGround)
        {
            PerformJump();
        }
    }

    void OnAttack(InputValue value)
    {
        if (!value.isPressed || !isAlive) return;

        if (!isInWater)
        {
            Instantiate(
                arrow,
                bow.position,
                bow.rotation
                );
        }
    }

    void PerformJump()
    {
        if (isInWater) { return; }

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

    private void Swim()
    {
        if (!isInWater)
        {
            playerRigidBody2D.gravityScale = startingGravityScale;
            return;
        }

        playerRigidBody2D.gravityScale = swimGravityScale;

        playerRigidBody2D.linearVelocity = new Vector2(
            moveInput.x * swimSpeed,
            moveInput.y * swimUpSpeed
        );
    }

    private void UpdateAnimation()
    {
        playerAnimator.SetBool("isRunning", playerHasHorizontalSpeed);
        playerAnimator.SetBool("isClimbing", playerHasVerticalSpeed && isInClimbingArea);
        playerAnimator.SetBool("isSwimming", isInWater);
    }

    private void Die()
    {
        if ((isTouchingEnemies || isTouchingHazards) && isAlive)
        {
            isAlive = false;
            playerAnimator.SetTrigger("Dying");

            StartCoroutine(DeathFlash());

            impulseSource.GenerateImpulse();
            StartCoroutine(DeathFreeze());
            StartCoroutine(DeathZoom());

            playerRigidBody2D.linearVelocity = deathKick;
            Invoke("DisablePlayer", 0.75f);
        }
    }

    IEnumerator DeathFreeze()
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(0.15f);
        Time.timeScale = 1f;
    }

    IEnumerator DeathZoom()
    {
        float start = cinemachineCamera.Lens.OrthographicSize;
        float target = 5f;

        for (float t = 0; t < 1; t += Time.deltaTime)
        {
            cinemachineCamera.Lens.OrthographicSize =
                Mathf.Lerp(start, target, t);

            yield return null;
        }
    }

    IEnumerator DeathFlash()
    {
        Color color = deathOverlay.color;

        color.a = 0f;
        deathOverlay.color = color;

        float duration = 0.15f;

        for (float t = 0; t < duration; t += Time.unscaledDeltaTime)
        {
            color.a = Mathf.Lerp(0f, 0.7f, t / duration);
            deathOverlay.color = color;
            yield return null;
        }

        color.a = 0.7f;
        deathOverlay.color = color;
    }

    private void DisablePlayer()
    {
        playerRigidBody2D.simulated = false;
    }
}
