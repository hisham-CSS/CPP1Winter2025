using UnityEngine;

public class Powerup : MonoBehaviour, IPickup
{
    Rigidbody2D rb;
    public void Pickup()
    {
        GameManager.Instance.PlayerInstance.SpeedChange();
        Destroy(gameObject);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = new Vector2(-2, 2);
    }

    void Update()
    {
        rb.linearVelocityX = -2;
    }
}
