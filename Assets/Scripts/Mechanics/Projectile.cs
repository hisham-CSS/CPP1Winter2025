using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    [SerializeField, Range(1, 20)] private float lifetime = 1.0f;
    [SerializeField, Range(1, 20)] private int damage = 20;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    public void SetVelocity(Vector2 velocity)
    {
        GetComponent<Rigidbody2D>().linearVelocity = velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.CompareTag("pProj"))
        {
            Enemy e = collision.gameObject.GetComponent<Enemy>();
            if (e != null)
            {
                e.TakeDamage(damage);
                Destroy(gameObject);
            } 
        }

        if (gameObject.CompareTag("eProj") && collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.lives--;
            Destroy(gameObject);
        }
    }
}
