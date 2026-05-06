using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PotionInteraction : MonoBehaviour, IInteractable
{
    [Header("Audio")]
    [SerializeField] private AudioClip sonBoisson;
    [SerializeField] private AudioSource musiqueAmbiante;

    [Header("Victoire")]
    [Tooltip("Nom exact du GameObject de l'écran de victoire dans la scène.")]
    [SerializeField] private string nomEcranVictoire = "Victoire";

    private AudioSource _audioSource;
    private GameObject _ecranVictoire;
    private bool _utilisee = false;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();

        if (musiqueAmbiante == null)
        {
            CompteARebours chrono = FindFirstObjectByType<CompteARebours>();
            if (chrono != null)
                musiqueAmbiante = chrono.GetComponent<AudioSource>();
        }

        foreach (Transform t in FindObjectsByType<Transform>(FindObjectsInactive.Include, FindObjectsSortMode.None))
        {
            if (t.gameObject.name == nomEcranVictoire && t.gameObject.scene == gameObject.scene)
            {
                _ecranVictoire = t.gameObject;
                break;
            }
        }

        if (_ecranVictoire == null)
            Debug.LogWarning($"[PotionInteraction] GameObject '{nomEcranVictoire}' introuvable dans la scène.");
    }

    public void Interact(GameObject interactor)
    {
        if (_utilisee) return;
        _utilisee = true;
        StartCoroutine(BoissonEtVictoire());
    }

    private IEnumerator BoissonEtVictoire()
    {
        if (musiqueAmbiante != null) musiqueAmbiante.Stop();
        if (sonBoisson != null)
            _audioSource.PlayOneShot(sonBoisson);

        float duree = sonBoisson != null ? sonBoisson.length : 0f;
        yield return new WaitForSecondsRealtime(duree);

        if (_ecranVictoire != null)
            _ecranVictoire.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
    }

    public string GetInteractionPrompt()
    {
        return "[E] Boire la potion";
    }
}
