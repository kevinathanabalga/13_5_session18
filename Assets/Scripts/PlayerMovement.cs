using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 7f;

    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private float horizontalInput;
    private bool jumpRequested;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        animator.SetFloat("Speed", Mathf.Abs(horizontalInput));

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpRequested = true;
        }

        SpriteFlip(horizontalInput);
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(
            horizontalInput * speed,
            rb.linearVelocity.y
        );

        bool isGrounded = Physics2D.OverlapCircle(
            groundCheck.position,
            groundCheckRadius,
            groundLayer
        );

        if (jumpRequested && isGrounded)
        {
            rb.linearVelocity = new Vector2(
                rb.linearVelocity.x,
                0f
            );

            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        jumpRequested = false;
    }

    private void SpriteFlip(float horizontalInput)
    {
        if (horizontalInput < 0)
            spriteRenderer.flipX = true;
        else if (horizontalInput > 0)
            spriteRenderer.flipX = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null)
            return;

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(
            groundCheck.position,
            groundCheckRadius
        );
    }
}