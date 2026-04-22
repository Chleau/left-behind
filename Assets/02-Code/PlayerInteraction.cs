using UnityEngine;
using TMPro; // Nécessite le package TextMeshPro

/// <summary>
/// À attacher sur le GameObject du joueur (celui qui a aussi l'Inventory).
/// Gère le Raycast depuis la caméra first-person et déclenche les interactions.
/// </summary>
[RequireComponent(typeof(Inventory))]
public class PlayerInteraction : MonoBehaviour
{
    [Header("Paramètres du Raycast")]
    [SerializeField] private Camera playerCamera; // Optionnel : auto-détecté si vide
    [SerializeField] private float interactDistance = 3f;
    [Tooltip("Cocher les layers 'Interactable' et 'Examinable' uniquement.")]
    [SerializeField] private LayerMask interactionLayers;

    [Header("Touche d'interaction")]
    [SerializeField] private KeyCode interactKey = KeyCode.E;

    [Header("UI - Prompt d'interaction")]
    [SerializeField] private TextMeshProUGUI interactionPromptUI;

    // L'objet interactable actuellement visé par le joueur (null si objet inutile)
    private IInteractable _currentTarget;

    private void Awake()
    {
        // Si la caméra n'est pas assignée manuellement, on la cherche automatiquement
        if (playerCamera == null)
            playerCamera = GetComponentInChildren<Camera>();

        if (playerCamera == null)
            Debug.LogError("[PlayerInteraction] Aucune Camera trouvée sur le joueur ou ses enfants !");

        if (interactionPromptUI != null)
            interactionPromptUI.gameObject.SetActive(false);
    }

    private void Update()
    {
        ScanForInteractable();

        // Si un objet est visé et que le joueur appuie sur la touche
        if (_currentTarget != null && Input.GetKeyDown(interactKey))
        {
            _currentTarget.Interact(gameObject);
        }
    }

    /// <summary>
    /// Lance un Raycast depuis la caméra. Met à jour _currentTarget et le prompt UI.
    /// </summary>
    private void ScanForInteractable()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);

        // Le raycast ne touche QUE les layers Interactable et Examinable
        bool didHit = Physics.Raycast(ray, out RaycastHit hit, interactDistance, interactionLayers);
        Debug.DrawRay(ray.origin, ray.direction * interactDistance, didHit ? Color.green : Color.red);

        if (didHit)
        {
            // Cas 1 : objet utile (implémente IInteractable)
            if (hit.collider.TryGetComponent(out IInteractable interactable))
            {
                _currentTarget = interactable;
                ShowPrompt(_currentTarget.GetInteractionPrompt());
                return;
            }

            // Cas 2 : objet inutile (a un ExaminableObject)
            if (hit.collider.TryGetComponent(out ExaminableObject examinable))
            {
                _currentTarget = null; // Pas d'interaction possible
                ShowPrompt(examinable.examineMessage);
                return;
            }
        }

        // Rien de reconnu : cache le prompt
        _currentTarget = null;
        HidePrompt();
    }

    /// <summary>
    /// Affiche le texte de prompt dans le HUD.
    /// </summary>
    private void ShowPrompt(string message)
    {
        if (interactionPromptUI == null) return;
        interactionPromptUI.gameObject.SetActive(true);
        interactionPromptUI.text = message;
    }

    /// <summary>
    /// Cache le prompt du HUD.
    /// </summary>
    private void HidePrompt()
    {
        if (interactionPromptUI == null) return;
        interactionPromptUI.gameObject.SetActive(false);
    }
}
