using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/Player Data")]
public class PlayerData : ScriptableObject
{
   public int currentLifePoints = 0; 
   public int maxLifePoints = 3; 

   public void TakeDamage(int damage)
   {
       currentLifePoints = Mathf.Max(0, currentLifePoints - damage);
   }
}
