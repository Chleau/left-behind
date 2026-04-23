using UnityEngine;
using System.Collections;

public class CoffreSimple : MonoBehaviour, IInteractable
{
    [Header("Animation")]
    public Animator animatorCoffre;
    public string nomDuBooleen = "open";

    [Header("UI")]
    public GameObject canvaInutile; 
    public string messagePrompt = "Examiner le coffre";

    private bool estOuvert = false;

    public void Interact(GameObject interactor)
    {
        if (estOuvert) return;

        // On ouvre le coffre
        if (animatorCoffre != null)
        {
            animatorCoffre.SetBool(nomDuBooleen, true);
            estOuvert = true;
        }

        // On lance l'affichage temporaire
        if (canvaInutile != null)
        {
            StartCoroutine(AffichageTemporaire());
        }
    }

    private IEnumerator AffichageTemporaire()
    {
        canvaInutile.SetActive(true); // On l'allume
        yield return new WaitForSeconds(2f); // On attend 2 secondes 
        canvaInutile.SetActive(false); // On l'éteint
    }

    public string GetInteractionPrompt()
    {
        return estOuvert ? "" : messagePrompt;
    }
}