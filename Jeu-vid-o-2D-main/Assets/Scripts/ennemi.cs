using UnityEngine;

public class Ennemi : MonoBehaviour
{
    // Tableau pour stocker les points de contact lors d'une collision
    private ContactPoint2D[] listeContact = new ContactPoint2D[1];

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision détectée");

        // Vérifie si l'objet entré en collision a le tag "Player"
        if (collision.gameObject.CompareTag("Player"))
        {
            // Obtient les points de contact de la collision
            collision.GetContacts(listeContact);

            // Vérifie si le premier point de contact a une normale y inférieure à -0.5
            // Cela indique que le joueur est au-dessus de l'ennemi
            if (listeContact[0].normal.y < -0.5f)
            {
                Debug.Log("Éliminé !");
                Destroy(gameObject); // Détruit l'ennemi
            }
            else
            {
                // Si le joueur n'est pas au-dessus, inflige des dégâts au joueur
                PlayerVie playerVie = collision.gameObject.GetComponent<PlayerVie>();
                {
                    playerVie.Hurt(); // Appelle la méthode Hurt() pour infliger des dégâts au joueur
                }
            }
        }
    }
}
