using UnityEngine;

public class Jump : MonoBehaviour
{
    Rigidbody2D rb;
    PlayerController pc;
    AudioSource audioSource;

    [SerializeField, Range(2, 5)] private float jumpHeight = 5;
    [SerializeField, Range(1, 20)] private float jumpFallForce = 20;
    [SerializeField] private AudioClip jumpSound;

    float timeHeld;
    float maxHoldTime = 0.5f;
    float jumpInputTime = 0.0f;
    float calculatedJumpForce;

    public bool jumpCancelled = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pc = GetComponent<PlayerController>();
        audioSource = GetComponent<AudioSource>();

        calculatedJumpForce = Mathf.Sqrt(jumpHeight * -2 * (Physics2D.gravity.y * rb.gravityScale));
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale <= 0) return;

        if (pc.isGrounded) jumpCancelled = false;

        if (Input.GetButton("Jump"))
        {
            jumpInputTime = Time.time;
            timeHeld += Time.deltaTime;
        }

        if (Input.GetButtonUp("Jump"))
        {
            timeHeld = 0;
            jumpInputTime = 0;

            if (rb.linearVelocity.y < -10) return;
            jumpCancelled = true;
        }

        if (jumpInputTime != 0 && (jumpInputTime + timeHeld) < (jumpInputTime + maxHoldTime))
        {
            if (pc.isGrounded)
            {
                audioSource.PlayOneShot(jumpSound);
                rb.linearVelocity = Vector2.zero;
                rb.AddForce(new Vector2(0, calculatedJumpForce), ForceMode2D.Impulse);
            }
        }

        if (jumpCancelled) rb.AddForce(Vector2.down * jumpFallForce);
    }
}
