using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float torqueAmount = 1f;
    [SerializeField] float baseSpeed = 15f;
    [SerializeField] float slowSpeed = 10f;
    [SerializeField] float boostSpeed = 24f;
    [SerializeField] ParticleSystem powerUpParticleSystem;
    int activePowerUpCount = 0;
    private InputAction moveAction;
    private Rigidbody2D rigidBody2D;
    private SurfaceEffector2D surfaceEffector2D;
    private RotationEffect rotationEffect;
    private Vector2 moveVector;
    private ScoreManager scoreManager;

    private bool canControlPlayer = true;
    private float previousRotation = 0f;
    private float totalRotation = 0f;
    private static float rotationGap = 20f;

    public void DisableControls(bool disable)
    {
        canControlPlayer = !disable;
    }

    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        surfaceEffector2D = FindAnyObjectByType<SurfaceEffector2D>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        rotationEffect = FindAnyObjectByType<RotationEffect>();
        scoreManager = FindAnyObjectByType<ScoreManager>();
    }

    void Update()
    {
        if (canControlPlayer)
        {
            moveVector = moveAction.ReadValue<Vector2>();

            RotatePlayer();
            BoostPlayer();
            CalculateFlips();
        }
    }

    void RotatePlayer()
    {
        if (moveVector.x != 0) //press < or >
        {
            bool rotateToLeft = moveVector.x < 0;
            rigidBody2D.AddTorque(torqueAmount * (rotateToLeft ? 1 : -1));
        }
    }

    void BoostPlayer()
    {
        if (moveVector.y > 0) // press Up ^
        {
            surfaceEffector2D.speed = boostSpeed;
        }
        else
        {
            surfaceEffector2D.speed = baseSpeed;
        }

        if (moveVector.y < 0)
        { // press Down 
            surfaceEffector2D.speed = slowSpeed;
        }
        else
        {
            surfaceEffector2D.speed = baseSpeed;
        }
    }

    private void CalculateFlips()
    {
        float currentRotation = transform.rotation.eulerAngles.z;

        totalRotation += Mathf.DeltaAngle(previousRotation, currentRotation);

        if (totalRotation > (360 - rotationGap) || totalRotation < (-360 + rotationGap))
        {
            scoreManager.AddScore(100);

            totalRotation = 0;
            rotationEffect.PlayAnimationForRotation();
        }

        previousRotation = currentRotation;
    }

    public void ActivatePowerUp(PowerupSO powerup)
    {
        powerUpParticleSystem.Play();
        activePowerUpCount += 1;
        
        if (powerup.getPowerUpType() == "speed")
        {
            baseSpeed += powerup.getValueChanged();
            boostSpeed += powerup.getValueChanged();
        }
        else if (powerup.getPowerUpType() == "torque")
        {
            torqueAmount += powerup.getValueChanged();
        }
    }

    public void DeactivatePowerUp(PowerupSO powerup)
    {
        activePowerUpCount -= 1;
        if (activePowerUpCount == 0)
        {
            powerUpParticleSystem.Stop();
        }

        if (powerup.getPowerUpType() == "speed")
        {
            baseSpeed -= powerup.getValueChanged();
            boostSpeed -= powerup.getValueChanged();
        }
        else if (powerup.getPowerUpType() == "torque")
        {
            torqueAmount -= powerup.getValueChanged();
        }
    }
}
