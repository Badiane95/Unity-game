using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed = 5f;
    public float moveDirectionX = 0f;
    public float jumpForce = 7f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public bool isGrounded = false;
    public LayerMask listGroundLayers;
    public int maxAllowedJumps = 2;
    public int currentNumberJumps = 0;
    public bool isFacingRight = true;

    public VoidEventChannel onPlayerDeath;

    private void OnEnable()
    {
        onPlayerDeath.OnEventRaised += Die;
    }

    private void OnDisable()
    {
        onPlayerDeath.OnEventRaised -= Die;
    }

    void Start()
    {
        // Initialisation ou configuration supplémentaire si nécessaire
    }

    void Die()
    {
        bc.enabled = false;
        rb.bodyType.GetType = Rigidbody2DType.Static;
        enabled = false;
    }

    void Update()
    {
        moveDirectionX = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && currentNumberJumps < maxAllowedJumps)
        {
            Jump();
            currentNumberJumps += 1;
        }

        if (isGrounded && !Input.GetButton("Jump"))
        {
            currentNumberJumps = 0;
        }

        Flip();
    }

    void Flip()
    {
        if ((moveDirectionX > 0 && !isFacingRight) || (moveDirectionX < 0 && isFacingRight))
        {
            transform.Rotate(0f, 180f, 0f);
            isFacingRight = !isFacingRight;
        }
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveDirectionX * moveSpeed, rb.linearVelocity.y);
        isGrounded = IsGrounded();
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, listGroundLayers);
    }

    private void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
