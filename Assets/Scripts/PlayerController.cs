using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]
[RequireComponent(typeof(GroundCheck), typeof(Jump))] 
public class PlayerController : MonoBehaviour
{
    //component references
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;
    private BoxCollider2D bc;
    private GroundCheck gndChk;

    //movement variables
    [Range(3, 10)]
    public float speed = 5.0f;
    public float jumpForce = 10.0f;

    public bool isGrounded = false;

    private Vector2 boxColliderOffset;
    private Vector2 boxColliderFlippedOffset;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        bc = GetComponent<BoxCollider2D>();
        gndChk = GetComponent<GroundCheck>();

        boxColliderOffset = bc.offset;
        boxColliderFlippedOffset = new Vector2(-boxColliderOffset.x, boxColliderOffset.y);

        if (jumpForce < 0) jumpForce = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorClipInfo[] curPlayingClips = anim.GetCurrentAnimatorClipInfo(0);
        CheckIsGrounded();

        float hInput = Input.GetAxis("Horizontal");

        if (curPlayingClips.Length > 0)
        {
            if (!(curPlayingClips[0].clip.name == "Fire"))
            {
                rb.linearVelocity = new Vector2(hInput * speed, rb.linearVelocity.y);

                if (Input.GetButtonDown("Fire1") && isGrounded) anim.SetTrigger("Fire");
                if (Input.GetButtonDown("Fire1") && !isGrounded) anim.SetTrigger("JumpAttack");
            }
            else
            {
                rb.linearVelocity = Vector2.zero;
            }
        }

        //sprite flipping
        if (hInput != 0) sr.flipX = (hInput < 0);
        //if (hInput > 0 && sr.flipX || hInput < 0 && !sr.flipX) sr.flipX = !sr.flipX;

        bc.offset = (sr.flipX) ? boxColliderFlippedOffset : boxColliderOffset;

        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("speed", Mathf.Abs(hInput));
    }

    void CheckIsGrounded()
    {
        if (!isGrounded)
        {
            if (rb.linearVelocity.y <= 0) isGrounded = gndChk.isGrounded();
        }
        else isGrounded = gndChk.isGrounded();
    }

    public void ResetRigidbody()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PowerUp"))
        {
            //do something
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        
    }
}
