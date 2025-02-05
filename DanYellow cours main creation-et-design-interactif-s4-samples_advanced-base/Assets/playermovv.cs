using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // Référence au composant Rigidbody2D
    public Rigidbody2D rb;

    // Variable pour stocker la direction horizontale du mouvement
    public float moveDirectionX = 0f;

    // Vitesse de déplacement du joueur
    public float moveSpeed = 10f;

    // Force de saut
    public float jumpForce = 5f;

    // Start est appelé avant la première mise à jour
    void Start()
    {
        // Vérifier si le Rigidbody2D est assigné
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
    }

    // Update est appelé une fois par frame
    void Update()
    {
        // Récupérer l'input horizontal (-1 à 1)
        moveDirectionX = Input.GetAxis("Horizontal");

        // Vérifier si le joueur veut sauter
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    // FixedUpdate est utilisé pour la physique, appelé à intervalle fixe
    private void FixedUpdate()
    {
        // Appliquer le mouvement au Rigidbody2D
        rb.linearVelocity = new Vector2(moveDirectionX * moveSpeed, rb.linearVelocity.y);
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }
}
