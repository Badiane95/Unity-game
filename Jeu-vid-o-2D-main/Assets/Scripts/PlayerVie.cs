using UnityEngine;
using System.Collections;

public class PlayerVie : MonoBehaviour
{
    // Données du joueur
    public PlayerData dataPlayer;

    // Paramètres d'invulnérabilité
    public bool isInvulnerable = false;
    public float invulnerableTime = 2.25f;
    public float invulnerableFlash = 0.2f;

    // Référence au SpriteRenderer du joueur
    public SpriteRenderer sr;

    public VoidEventChannel onPlayerDeath;

    void Start()
    {
        // Initialise les données du joueur
        if (dataPlayer == null)
        {
            dataPlayer = ScriptableObject.CreateInstance<PlayerData>();
        }
        dataPlayer.currentLifePoints = dataPlayer.maxLifePoints;
    }

    // Méthode pour infliger des dégâts au joueur
    public void Hurt(int damage = 1)
    {
        if (isInvulnerable) return;

        // Réduit les points de vie actuels en fonction des dégâts reçus
        dataPlayer.TakeDamage(damage);

        // Vérifie si les points de vie sont tombés à 0 ou moins
        if (dataPlayer.currentLifePoints <= 0)
        {
            onPlayerDeath.Raise();
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
        Color startColor = sr.color;

        WaitForSeconds invulnerableFlashWait = new WaitForSeconds(invulnerableFlash);

        for (float i = 0; i < invulnerableTime; i += invulnerableFlash)
        {
            sr.color = sr.color.a == 1 ? Color.clear : startColor;
            yield return invulnerableFlashWait;
        }
        sr.color = startColor; // Assure que le sprite est visible à la fin
        isInvulnerable = false;
    }
}
