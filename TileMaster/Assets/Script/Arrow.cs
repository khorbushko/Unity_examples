using Unity.VisualScripting;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Rigidbody2D rigidBody2D;
    [SerializeField] float speed = 1f;
    private float xSpeed = 1f;
    public AudioClip arrowAudioClip;

    PlayerMovement player;
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        player = FindAnyObjectByType<PlayerMovement>();
        xSpeed = player.transform.localScale.x * speed; // track rotation of player

        transform.localScale = new Vector2(
               player.transform.localScale.x,
               1f
           ); // rotate arrow
           
        GeneralSoundController.Instance.PlaySound(arrowAudioClip);
    }

    void Update()
    {
        rigidBody2D.linearVelocity = new Vector2(xSpeed, rigidBody2D.linearVelocity.y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Destroy(other.gameObject);
        }

        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }
}
