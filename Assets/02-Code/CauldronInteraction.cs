using UnityEngine;

/// <summary>
/// À attacher sur le GameObject du Chaudron.
/// Vérifie les ingrédients dans l'inventaire, joue un son et spawne la potion.
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class CauldronInteraction : MonoBehaviour, IInteractable
{
    [Header("Ingrédients requis")]
    [Tooltip("Glisse ici les ItemData nécessaires pour fabriquer la potion.")]
    [SerializeField] private ItemData[] ingredientsRequis;

    [Header("Potion à spawner")]
    [Tooltip("Prefab de la potion qui apparaîtra près du chaudron.")]
    [SerializeField] private GameObject potionPrefab;
    [Tooltip("Position où la potion apparaîtra (crée un GameObject vide près du chaudron).")]
    [SerializeField] private Transform spawnPoint;

    [Header("Audio")]
    [Tooltip("Son de bouillonnement / succès quand la potion est créée.")]
    [SerializeField] private AudioClip sonBrassage;
    [Tooltip("Son d'erreur si les ingrédients manquent.")]
    [SerializeField] private AudioClip sonErreur;

    private AudioSource _audioSource;
    private bool _potionDejaCreee = false;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void Interact(GameObject interactor)
    {
        if (_potionDejaCreee)
        {
            Debug.Log("[Chaudron] La potion a déjà été créée.");
            return;
        }

        Inventory inventory = interactor.GetComponent<Inventory>();
        if (inventory == null) return;

        // Vérifie que tous les ingrédients sont présents
        foreach (ItemData ingredient in ingredientsRequis)
        {
            if (!inventory.HasItem(ingredient))
            {
                Debug.Log($"[Chaudron] Ingrédient manquant : {ingredient.itemName}");
                _audioSource.PlayOneShot(sonErreur);
                return;
            }
        }

        // Consomme les ingrédients
        foreach (ItemData ingredient in ingredientsRequis)
            inventory.RemoveItem(ingredient);

        // Joue le son de brassage
        _audioSource.PlayOneShot(sonBrassage);

        // Spawne la potion
        Vector3 position = spawnPoint != null ? spawnPoint.position : transform.position + Vector3.up;
        Instantiate(potionPrefab, position, Quaternion.identity);

        _potionDejaCreee = true;
        Debug.Log("[Chaudron] Potion créée !");
    }

    public string GetInteractionPrompt()
    {
        if (_potionDejaCreee) return "Le chaudron est vide.";
        return "[E] Utiliser le chaudron";
    }
}
