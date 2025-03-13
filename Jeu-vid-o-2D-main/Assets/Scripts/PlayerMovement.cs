using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public BoxCollider2D bc;
    public float moveSpeed = 5f; //Vitesse de déplacement en float
    public float moveDirectionX = 0f; 
    public float jumpForce = 7f; //Puissance du saut

    public Transform groundCheck; 

    public float groundCheckRadius = 0.2f; 
    public Animator animator;
    public bool isGrounded = false; 
    public LayerMask listGroundLayers;
    public int maxAllowedJumps = 2;
    public int currentNumberJumps= 0;

    private bool isFacingRight = true;

    public bool isPaused = false;

    public VoidEventChannel onPlayerDeath;
    public VoidEventChannel onPause;
    public VoidEventChannel onResume;
    
    private void OnEnable()
    {
        onPlayerDeath.OnEventRaised += Die;
        onResume.OnEventRaised += OnResume;
        onPause.OnEventRaised += OnPause;

    }

    private void OnDisable()
    {
        onPlayerDeath.OnEventRaised -= Die;
        onResume.OnEventRaised -= OnResume;
        onPause.OnEventRaised -= OnPause;
    }

    void Die() {
        enabled = false;
        rb.bodyType = RigidbodyType2D.Static;
        bc.enabled = false;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isPaused) {
            return;
        }
        animator.SetFloat("VelocityX", Mathf.Abs(rb.linearVelocityX));
        Debug.Log("linearVelocityX " + Mathf.Abs(rb.linearVelocityX));
        animator.SetFloat("VelocityY", rb.linearVelocityY);
        animator.SetBool("IsGrounded", isGrounded);
        moveDirectionX = Input.GetAxis("Horizontal"); //Récupère un float de la direction dans laquel le jouer essaie de diriger (bouton Q et D, fleche gauche et droite et Joystick)
        /*if (Input.GetKeyDown(KeyCode.Space)) {
            Jump();
        }*/
        if(Input.GetButtonDown("Jump") && currentNumberJumps < maxAllowedJumps) {           //Si le joueur clique sur le bouton de saut il saute
            Jump();
            currentNumberJumps +=1;
            if(currentNumberJumps > 1) {
                animator.SetTrigger("DoubleJump");
            }
        }

        if (isGrounded && !Input.GetButton("Jump")) {
            currentNumberJumps = 0;
        }
        Flip();
    }

    public void Jump() {
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
    private void OnDrawGizmos() {
        if(groundCheck != null) {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(
                groundCheck.position,
                groundCheckRadius
            );
        }
    }

    public void OnPause() {
        isPaused = true;
    }
    public void OnResume() {
        isPaused = false;
    }

    private void Flip() {
        if ((moveDirectionX > 0 && !isFacingRight) || (moveDirectionX < 0 && isFacingRight)) {
            transform.Rotate(0,180,0);
            isFacingRight = !isFacingRight;
        } 
    }
}
