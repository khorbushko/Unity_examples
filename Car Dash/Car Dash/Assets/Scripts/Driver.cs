using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Driver : MonoBehaviour
{
    [SerializeField] float steertSpeed = 200f;
    [SerializeField] float moveSpeed = 20f;
    [SerializeField] float boostSpeed = 3f;

    private float boostSpeedModifier = 1f;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float steer = 0f;
        float move = 0f;

        if (Keyboard.current.wKey.isPressed)
        {
            move = 1f;
        }
        else if (Keyboard.current.sKey.isPressed)
        {
            move = -1f;
        }

        if (Keyboard.current.dKey.isPressed)
        {
            steer = -1f;
        }
        else if (Keyboard.current.aKey.isPressed)
        {
            steer = 1f;
        }

        float moveAmount = moveSpeed * move * Time.deltaTime * boostSpeedModifier;
        float steerAmount = steertSpeed * steer * Time.deltaTime;

        transform.Translate(0f, moveAmount, 0f);
        transform.Rotate(0, 0, steerAmount);
    }

    public void ApplyBoost()
    {
        boostSpeedModifier = boostSpeed;
    }

    public void RemoveBoost()
    {
        boostSpeedModifier = 1f;
    }
}
