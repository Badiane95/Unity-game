using UnityEngine;
using System.Collections;

public class PlayerVie : MonoBehaviour
{
    // Points de vie maximum du joueur
    public int maxLifePoints = 3;

    // Points de vie actuels du joueur
    public int currentLifePoints;

    // Paramètres d'invulnérabilité
    public bool isInvulnerable = false;
    public float invulnerableTime = 2.25f;
    public float invulnerableFlash = 0.2f;

    // Référence au SpriteRenderer du joueur
    public SpriteRenderer sr;

    void Start()
    {
        // Initialise les points de vie actuels avec les points de vie maximum
        currentLifePoints = maxLifePoints;
    }

    // Méthode pour infliger des dégâts au joueur
    public void Hurt(int damage = 1)
    {
        if (isInvulnerable) return;

        // Réduit les points de vie actuels en fonction des dégâts reçus
        currentLifePoints -= damage;

        // Vérifie si les points de vie sont tombés à 0 ou moins
        if (currentLifePoints <= 0)
        {
            currentLifePoints = 0; // Empêche les points de vie d'être négatifs
            Die(); // Appelle la méthode pour gérer la mort du joueur
        }
        else
        {
            StartCoroutine(Invulnerable());
        }
    }

    // Méthode appelée lorsque le joueur meurt
    private void Die()
    {
        Debug.Log("Le joueur est mort !");
        Destroy(gameObject); // Détruit l'objet joueur
    }

    // Coroutine pour gérer l'invulnérabilité temporaire
    IEnumerator Invulnerable()
    {
        isInvulnerable = true;
        Color stratColor = sr.color;

        WaitForSeconds invulnerableFlashWait= 
        new WaitForSeconds (invulnerableFlash);

        for (float i = 0; i < invulnerableTime; i += invulnerableFlash)
        {
            if (sr.color.a == 1)
            {
                sr.color = Color.clear;
            }
            else
            {
                sr.color = stratColor;
            }
            yield return new WaitForSeconds(invulnerableFlash);
        }
       sr.color = stratColor; // Assure que le sprite est visible à la fin
        isInvulnerable = false;
    }
}
