using UnityEngine;

/// <summary>
/// Contrat partagé entre Dev A et Dev B.
/// Tout objet interactable de la scène doit implémenter cette interface.
/// </summary>
public interface IInteractable
{
    /// <summary>
    /// Appelé par PlayerInteraction quand le joueur appuie sur la touche d'interaction.
    /// Le paramètre 'interactor' est le GameObject du joueur, utile pour accéder à l'Inventory.
    /// </summary>
    void Interact(GameObject interactor);

    /// <summary>
    /// Texte affiché dans le HUD quand le joueur vise cet objet.
    /// Ex : "Appuyer sur E pour ramasser la clé"
    /// </summary>
    string GetInteractionPrompt();
}
