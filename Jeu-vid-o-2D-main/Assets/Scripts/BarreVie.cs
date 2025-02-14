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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Initialisation si nécessaire
    }

    // Update is called once per frame
    void Update()
    {
        // Calcul du ratio de vie actuelle
        float lifeRatio = (float)playerVie.currentLifePoints / (float)playerVie.maxLifePoints;

        // Mise à jour du remplissage de l'image
        fillImage.fillAmount = lifeRatio;

        // Mise à jour de la couleur de l'image en fonction du ratio de vie
        fillImage.color = lifeColorGradient.Evaluate(lifeRatio);
    }
}
