using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Stocke les objets ramassés par le joueur.
/// Notifie l'UI via un événement à chaque changement.
/// </summary>
public class Inventory : MonoBehaviour
{
    // Abonne-toi à cet événement pour réagir aux changements d'inventaire (ex: HUD)
    public event Action OnInventoryChanged;

    [SerializeField] private List<ItemData> items = new List<ItemData>();

    public IReadOnlyList<ItemData> Items => items;

    public void AddItem(ItemData item)
    {
        if (item == null) return;
        items.Add(item);
        Debug.Log($"[Inventory] Ramassé : {item.itemName}. Total : {items.Count}");
        OnInventoryChanged?.Invoke(); // Notifie le HUD
    }

    public bool RemoveItem(ItemData item)
    {
        bool removed = items.Remove(item);
        if (removed)
            OnInventoryChanged?.Invoke(); // Notifie le HUD
        return removed;
    }

    public bool HasItem(ItemData item)
    {
        return items.Contains(item);
    }
}
