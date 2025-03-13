using UnityEngine;
using UnityEngine.UI;

public class BarreVie : MonoBehaviour
{
    // Référence à l'image de remplissage de la barre de vie
    public Image fillImage;

    // Référence au script gérant la vie du joueur
    public PlayerVie playerVie;

    // Gradient pour changer la couleur de la barre de vie en fonction du niveau de vie
    public Gradient lifeColorGradient;
    
        public PlayerData dataPlayer;

    void Start()
    {
        // Assurez-vous que playerVie est assigné dans l'inspecteur Unity
    }

    void Update()
    {
        if (playerVie != null && playerVie.dataPlayer != null)
        {
            // Calcul du ratio de vie actuelle
            float lifeRatio = (float)playerVie.dataPlayer.currentLifePoints / (float)playerVie.dataPlayer.maxLifePoints;

            // Mise à jour du remplissage de l'image
            fillImage.fillAmount = lifeRatio;

            // Mise à jour de la couleur de l'image en fonction du ratio de vie
            fillImage.color = lifeColorGradient.Evaluate(lifeRatio);
        }
    }
}
