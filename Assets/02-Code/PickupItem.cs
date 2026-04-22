using UnityEngine;

/// <summary>
/// À attacher sur tout objet ramassable de la scène (clé, poudre...).
/// Configure 'itemData' dans l'inspecteur avec le ScriptableObject correspondant.
/// </summary>
[RequireComponent(typeof(Collider))]
public class PickupItem : MonoBehaviour, IInteractable
{
    [Tooltip("Le ScriptableObject ItemData qui décrit cet objet (nom, icône...)")]
    public ItemData itemData;

    /// <summary>
    /// Ajoute l'item à l'inventaire du joueur et supprime l'objet de la scène.
    /// </summary>
    public void Interact(GameObject interactor)
    {
        Inventory inventory = interactor.GetComponent<Inventory>();
        if (inventory == null)
        {
            Debug.LogWarning("[PickupItem] Pas de composant Inventory sur l'interacteur.");
            return;
        }

        inventory.AddItem(itemData);
        Destroy(gameObject); // L'objet disparaît de la scène une fois ramassé
    }

    /// <summary>
    /// Texte affiché dans le HUD quand le joueur vise cet objet.
    /// </summary>
    public string GetInteractionPrompt()
    {
        string nom = itemData != null ? itemData.itemName : "l'objet";
        return $"[E] Ramasser {nom}";
    }
}
