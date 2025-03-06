using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed = 5f;
    private float moveDirectionX = 0f;
    public float jumpForce = 7f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public bool isGrounded;
    public LayerMask listGroundLayers;
    public int maxAllowedJumps = 2;
    public int currentNumberJumps = 0;
    public bool isFacingRight = true;

    public BoxCollider2D bc;
    public VoidEventChannel onPlayerDeath;
    public PauseMenu pauseMenu; // Référence au script PauseMenu

    public Animator animator;

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
        // Assurez-vous d'assigner la référence au PauseMenu dans l'éditeur Unity
    }

    void Die()
    {
        bc.enabled = false;
        rb.bodyType = RigidbodyType2D.Static;
        enabled = false;
    }

    void Update()
    {
        if (pauseMenu != null && pauseMenu.isPaused)
            return;

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

        // Mise à jour des paramètres de l'animator
        animator.SetFloat("VelocityX", Mathf.Abs(rb.linearVelocity.x));
        animator.SetFloat("VelocityY", rb.linearVelocity.y);
        animator.SetBool("isGrounded", isGrounded); // Utilisez SetBool si votre paramètre est un booléen
    }

    void Flip()
    {
        if ((moveDirectionX > 0 && !isFacingRight) || (moveDirectionX < 0 && isFacingRight))
        {
            isFacingRight = !isFacingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
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