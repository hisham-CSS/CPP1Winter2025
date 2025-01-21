using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    //component references
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;
    private BoxCollider2D bc;

    //movement variables
    [Range(3, 10)]
    public float speed = 5.0f;
    public float jumpForce = 10.0f;

    //groundcheck variables
    [Range(0.01f, 0.1f)]
    public float groundCheckRadius = 0.02f;
    public LayerMask isGroundLayer;
    public bool isGrounded = false;

    private Transform groundCheck;

    private Vector2 boxColliderOffset;
    private Vector2 boxColliderFlippedOffset;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        bc = GetComponent<BoxCollider2D>();

        boxColliderOffset = bc.offset;
        boxColliderFlippedOffset = new Vector2(-boxColliderOffset.x, boxColliderOffset.y);

        if (jumpForce < 0) jumpForce = 5.0f;

        //groundcheck initalization
        GameObject newGameObject = new GameObject();
        newGameObject.transform.SetParent(transform);
        newGameObject.transform.localPosition = Vector3.zero;
        newGameObject.name = "GroundCheck";
        groundCheck = newGameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        CheckIsGrounded();

        float hInput = Input.GetAxis("Horizontal");

        rb.linearVelocity = new Vector2(hInput * speed, rb.linearVelocity.y);

        if (Input.GetButtonDown("Jump") && isGrounded)
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        //sprite flipping
        if (hInput != 0) sr.flipX = (hInput < 0);
        //if (hInput > 0 && sr.flipX || hInput < 0 && !sr.flipX) sr.flipX = !sr.flipX;

        bc.offset = (sr.flipX) ? boxColliderFlippedOffset : boxColliderOffset;

        //if (sr.flipX) bc.offset = boxColliderFlippedOffset; else bc.offset = boxColliderOffset;

        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("speed", Mathf.Abs(hInput));
    }

    void CheckIsGrounded()
    {
        if (!isGrounded)
        {
            if (rb.linearVelocity.y <= 0) isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);
        }
        else isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);
    }
}
