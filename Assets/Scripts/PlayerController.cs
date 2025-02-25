using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor;
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
    private Coroutine speedChange = null;

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

    public void ResetRigidbody() => rb.bodyType = RigidbodyType2D.Dynamic;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Detect pickup
        IPickup pickup = collision.gameObject.GetComponent<IPickup>();
        if (pickup != null) pickup.Pickup();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Detect pickup
        IPickup pickup = collision.GetComponent<IPickup>();
        if (pickup != null) pickup.Pickup();

        if ((rb.linearVelocityY < 0) && collision.CompareTag("Squish"))
        {
            collision.enabled = false;
            collision.gameObject.GetComponentInParent<Enemy>().TakeDamage(9999, DamageType.JumpedOn);
            rb.linearVelocity = Vector2.zero;
            rb.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        
    }

    

    public void SpeedChange()
    {
        if (speedChange != null)
        {
            StopCoroutine(speedChange);
            speed /= 2;
        }
    
        speedChange = StartCoroutine(SpeedChangeCoroutine());
    }

    IEnumerator SpeedChangeCoroutine()
    {
        //do something immediately
        speed *= 2;
        Debug.Log($"Player Controller speed has changed to {speed}");

        yield return new WaitForSeconds(5.0f);

        //do something after 5 seconds
        speed /= 2;
        Debug.Log($"Player Controller speed has changed to {speed}");
    }
}
