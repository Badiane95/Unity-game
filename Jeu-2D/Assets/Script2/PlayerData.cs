using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/Variables/PlayerData")]
public class PlayerData : ScriptableObject
{
    public int currentLifePoints = 0;
    public int maxLifePoints = 3;

    // Méthode pour infliger des dégâts au joueur
    public void TakeDamage(int damage)
    {
        // Réduction des points de vie actuels
        currentLifePoints -= damage;

        // S'assurer que les points de vie ne descendent pas en dessous de zéro
        currentLifePoints = Mathf.Max(currentLifePoints, 0);

        Debug.Log($"Le joueur a pris {damage} dégâts. Points de vie restants : {currentLifePoints}");
    }
}
