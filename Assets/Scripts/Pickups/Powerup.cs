using UnityEngine;
using UnityEngine.Audio;

public class Powerup : MonoBehaviour, IPickup
{
    public AudioClip pickupSound;
    Rigidbody2D rb;
    AudioSource audioSource;

    public void Pickup()
    {
        GameManager.Instance.PlayerInstance.SpeedChange();
        audioSource.PlayOneShot(pickupSound);
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        Destroy(gameObject, pickupSound.length);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        rb.linearVelocity = new Vector2(-2, 2);
    }

    void Update()
    {
        rb.linearVelocityX = -2;
    }
}
