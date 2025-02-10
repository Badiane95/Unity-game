using UnityEngine;

public class EnnemiMov : MonoBehaviour
{
    // Référence au Rigidbody2D de l'ennemi
    public Rigidbody2D rb;

    // Vitesse de déplacement de l'ennemi
    public float moveSpeed = 3f;

    // Couches considérées comme obstacles ou sol
    public LayerMask listeObstacleLayers;

    // Référence au BoxCollider2D attaché à l'ennemi
    public BoxCollider2D bc;

    // Distance pour détecter les obstacles devant l'ennemi
    public float distanceDetection = 0.5f;

    // Direction du mouvement (par défaut vers la droite)
    private Vector3 moveDirection = Vector3.right;

    // Indique si l'ennemi fait face à droite
    public bool isFacingRight = true;

    // FixedUpdate est appelé à intervalles fixes, idéal pour la physique
    void FixedUpdate()
    {
        // Si l'ennemi est en l'air, ne rien faire
        if (rb.linearVelocity.y != 0)
        {
            return;
        }

        // Applique une vitesse constante à l'ennemi dans la direction spécifiée
        rb.linearVelocity = new Vector3(
            moveDirection.x * moveSpeed, // Multiplie la direction par la vitesse
            rb.linearVelocity.y                // Conserve la vitesse verticale actuelle
        );

        // Vérifie si l'ennemi a atteint un bord ou un obstacle
        if (HasNotTouchedGround() || HasCollisionWithObject())
        {
            Flip(); // Change de direction si nécessaire
        }
    }

    // Vérifie si l'ennemi a atteint un bord ou un obstacle
    bool HasNotTouchedGround()
    {
        // Position pour détecter le sol ou un obstacle devant l'ennemi
        Vector3 detectionPosition = new Vector3(
            bc.bounds.center.x + (transform.right.normalized.x * (bc.bounds.size.x / 2)), // Bord avant du BoxCollider2D
            bc.bounds.min.y // Bas du BoxCollider2D
        );

        // Vérifie s'il n'y a pas de sol ou d'obstacle à la position détectée
        return !Physics2D.OverlapCircle(
            detectionPosition,      // Position du cercle de détection
            0.1f,                   // Rayon du cercle (petit pour précision)
            listeObstacleLayers     // Couches à vérifier (sol/obstacles)
        );
    }

    // Vérifie s'il y a une collision avec un objet devant l'ennemi
    bool HasCollisionWithObject()
    {
        RaycastHit2D hit = Physics2D.Linecast(
            bc.bounds.center,                                      // Point de départ du Linecast (centre du BoxCollider)
            bc.bounds.center + new Vector3(distanceDetection * transform.right.normalized.x, 0), // Point d'arrivée du Linecast (devant l'ennemi)
            listeObstacleLayers                                    // Couches à vérifier pour la collision
        );

        return hit.transform != null;                             // Retourne vrai si un objet est détecté, sinon faux
    }

    // Change la direction de déplacement de l'ennemi
    void Flip()
    {
        moveDirection = -moveDirection;               // Inverse la direction du mouvement
        transform.Rotate(0f, 180f, 0f);               // Retourne visuellement l'ennemi
        isFacingRight = !isFacingRight;               // Inverse la direction à laquelle l'ennemi fait face
    }

    // Fonction pour dessiner des gizmos dans l'éditeur Unity (utile pour visualiser les zones de détection)
    private void OnDrawGizmos()
    {
        if (bc != null)
        {
            Gizmos.color = Color.red;                 // Définit la couleur des gizmos pour la détection des bords/sols

            Vector3 detectionPosition = new Vector3(
                bc.bounds.center.x + (transform.right.normalized.x * (bc.bounds.size.x / 2)),
                bc.bounds.min.y
            );

            Gizmos.DrawWireSphere(detectionPosition, 0.1f); // Dessine un cercle pour visualiser la détection des bords

            Gizmos.color = Color.blue; 
                           // Définit la couleur des gizmos pour le Linecast

            Gizmos.DrawLine(
                bc.bounds.center,
                bc.bounds.center + new Vector3(distanceDetection * transform.right.normalized.x, 0) // Ligne devant l'ennemi pour détecter les obstacles
            );
        }
    }
}
