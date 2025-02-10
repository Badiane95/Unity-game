using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Référence au Rigidbody2D pour appliquer des forces et gérer le mouvement
    public Rigidbody2D rb;

    // Vitesse de déplacement horizontale
    public float moveSpeed = 5f; 

    // Direction du mouvement sur l'axe X
    public float moveDirectionX = 0f;

    // Force appliquée lors du saut
    public float jumpForce = 7f;

    // Transform utilisé pour vérifier si le joueur est au sol
    public Transform groundCheck;

    // Rayon du cercle utilisé pour détecter le sol
    public float groundCheckRadius = 0.2f;

    // Booléen indiquant si le joueur est au sol
    public bool isGrounded = false;

    // Masque de couche pour identifier les objets considérés comme "sol"
    public LayerMask listGroundLayers;

    // Nombre maximum de sauts autorisés (double saut par exemple)
    public int maxAllowedJumps = 2;

    // Nombre actuel de sauts effectués
    public int currentNumberJumps = 0;

    // Indique si le joueur fait face à droite
    public bool isFacingRight = true;

    // Start est appelé une fois avant la première exécution d'Update après la création du MonoBehaviour
    void Start()
    {
        // Initialisation ou configuration supplémentaire si nécessaire
    }

    // Update est appelé une fois par frame
    void Update()
    {
        // Récupère l'entrée horizontale (clavier ou manette)
        moveDirectionX = Input.GetAxis("Horizontal");

        // Si le joueur appuie sur le bouton "Jump" et qu'il n'a pas dépassé le nombre maximum de sauts autorisés
        if (Input.GetButtonDown("Jump") && currentNumberJumps < maxAllowedJumps)
        {
            Jump();
            currentNumberJumps += 1; // Incrémente le compteur de sauts
        }

        // Réinitialise le compteur de sauts si le joueur est au sol et ne maintient pas le bouton "Jump"
        if (isGrounded && !Input.GetButton("Jump"))
        {
            currentNumberJumps = 0;
        }

        // Appelle la fonction pour retourner le personnage si nécessaire
        Flip();
    }

    // Fonction pour retourner le personnage en fonction de la direction du mouvement
    void Flip()
    {
        if ((moveDirectionX > 0 && !isFacingRight) || (moveDirectionX < 0 && isFacingRight))
        {
            transform.Rotate(0f, 180f, 0f); // Retourne le personnage horizontalement
            isFacingRight = !isFacingRight; // Inverse l'état du booléen
        }
    }

    // Fonction pour gérer le saut
    private void Jump()
    {
        rb.linearVelocity = new Vector2(
            rb.linearVelocity.x,  // Conserve la vitesse horizontale actuelle
            jumpForce       // Applique la force verticale pour sauter
        );
    }

    // FixedUpdate est appelé à intervalles fixes, idéal pour la physique
    private void FixedUpdate()
    {
        // Gère le déplacement horizontal du joueur
        rb.linearVelocity = new Vector2(
            moveDirectionX * moveSpeed,  // Multiplie la direction par la vitesse de déplacement
            rb.linearVelocity.y               // Conserve la vitesse verticale actuelle
        );

        // Vérifie si le joueur est au sol
        isGrounded = IsGrounded();
    }

    // Vérifie si le joueur est au sol en utilisant un cercle autour du point "groundCheck"
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(
            groundCheck.position,      // Position du cercle (point de vérification)
            groundCheckRadius,         // Rayon du cercle
            listGroundLayers           // Couches considérées comme "sol"
        );
    }

    // Fonction pour dessiner des gizmos dans l'éditeur Unity (utile pour visualiser les zones de détection)
    private void OnDrawGizmos()
    {
        if (groundCheck != null) 
        {
            Gizmos.color = Color.magenta;  // Définit la couleur des gizmos
            Gizmos.DrawWireSphere(
                groundCheck.position,     // Position du cercle à dessiner
                groundCheckRadius         // Rayon du cercle à dessiner
            );
        }
    }
}
