using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Gère l'affichage des slots d'inventaire dans le HUD.
/// À attacher sur un GameObject du Canvas.
/// </summary>
public class InventoryHUD : MonoBehaviour
{
    [Header("Référence à l'inventaire du joueur")]
    [SerializeField] private Inventory playerInventory;

    [Header("Slots UI (assigne tes Image[] dans l'ordre)")]
    [Tooltip("Les composants Image de chaque slot — un par item possible.")]
    [SerializeField] private Image[] slots;

    [Header("Visuel des slots")]
    [Tooltip("Icône affichée quand le slot est vide.")]
    [SerializeField] private Sprite emptySlotSprite;

    private void Start()
    {
        // Abonnement à l'événement de l'inventaire
        playerInventory.OnInventoryChanged += RefreshHUD;
        RefreshHUD(); // Initialise l'affichage
    }

    private void OnDestroy()
    {
        // Désabonnement pour éviter les memory leaks
        playerInventory.OnInventoryChanged -= RefreshHUD;
    }

    /// <summary>
    /// Met à jour tous les slots selon le contenu actuel de l'inventaire.
    /// </summary>
    private void RefreshHUD()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            bool hasItem = i < playerInventory.Items.Count;
            ItemData item = hasItem ? playerInventory.Items[i] : null;

            // Si l'item a une icône, on l'affiche; sinon on met l'icône de slot vide
            slots[i].sprite = (hasItem && item.icon != null) ? item.icon : emptySlotSprite;
            slots[i].color = hasItem ? Color.white : new Color(1, 1, 1, 0.3f); // Transparent si vide
        }
    }
}
