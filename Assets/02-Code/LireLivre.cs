using UnityEngine;

public class AffichageEtape4 : MonoBehaviour, IInteractable
{
    [Header("UI à afficher")]
    public GameObject canvaEtape4;
    public string messagePrompt = "Appuyer sur E pour lire le livre";

    private bool estEnTrainDeLire = false;
    private MonoBehaviour playerScript;

    public void Interact(GameObject interactor)
    {
        if (playerScript == null)
        {
            playerScript = interactor.GetComponent<MonoBehaviour>(); 
        }
        if (!estEnTrainDeLire)
        {
            OuvrirPage();
        }
        else
        {
            FermerPage();
        }
    }

    public string GetInteractionPrompt()
    {
        return estEnTrainDeLire ? "Appuyer sur E pour fermer" : messagePrompt;
    }

    void OuvrirPage()
    {
        estEnTrainDeLire = true;
        canvaEtape4.SetActive(true);
        
        if (playerScript != null) playerScript.enabled = false;
    }

    public void FermerPage()
    {
        estEnTrainDeLire = false;
        canvaEtape4.SetActive(false);
        if (playerScript != null) playerScript.enabled = true;
    }

    void Update()
    {
        // Si le joueur appuie sur Echap pendant qu'il lit, on ferme aussi
        if (estEnTrainDeLire && Input.GetKeyDown(KeyCode.Escape))
        {
            FermerPage();
        }
    }
}