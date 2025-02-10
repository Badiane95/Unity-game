using UnityEngine;

public class Ennemi : MonoBehaviour
{
    // Tableau pour stocker les points de contact lors d'une collision
    private ContactPoint2D[] listeContact = new ContactPoint2D[1];

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Vérifie si l'objet entré en collision a le tag "Player"
        if (collision.gameObject.CompareTag("Player"))
        {
            // Obtient les points de contact de la collision
            collision.GetContacts(listeContact);

            // Vérifie si le premier point de contact a une normale y inférieure à -0.5
            // Ce qui indique que le joueur est au-dessus de l'ennemi
            if (listeContact[0].normal.y < -0.5f)
            {
                Debug.Log("Éliminé !");
                // Ici, vous pouvez ajouter le code pour éliminer l'ennemi
                // Par exemple : Destroy(gameObject);
            }
        
        }
    }
}
