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
    public int currentNumberJumps= 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveDirectionX = Input.GetAxis("Horizontal");
        /*if (Input.GetKeyDown(KeyCode.Space)) {
            Jump();
        }*/
        if(Input.GetButtonDown("Jump") && currentNumberJumps < maxAllowedJumps) {
            Jump();
            currentNumberJumps +=1;
        }

        if (isGrounded && !Input.GetButton("Jump")) {
            currentNumberJumps = 0;
        }
    }

    private void Jump() {
        rb.linearVelocity = new Vector2 (
            rb.linearVelocity.x,
            jumpForce
        );
    }
    private void FixedUpdate() {
        rb.linearVelocity = new Vector2 (
            moveDirectionX * moveSpeed,
            rb.linearVelocity.y
        );
        isGrounded =IsGrounded();
    }

    private bool IsGrounded () {
        return Physics2D.OverlapCircle(
            groundCheck.position,
            groundCheckRadius,
            listGroundLayers
        );
    }
}
