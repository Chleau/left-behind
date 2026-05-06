using UnityEngine;

public class CoffreLivre : MonoBehaviour, IInteractable
{
    
    [Header("Animation")]
    public Animator animatorCoffre; // L'objet avec l'Animator

    [Header("Audio")] 
    public AudioSource audioCoffre;

    [Header("Contenu")]
    public GameObject livreAApparaitre; // Le livre Book03 avec la recette

    [Header("Condition de Clé")]
    public ItemData objetCleRequise; // Le fichier ItemData_Cle

    [Header("Textes HUD")]
    public string texteSansCle = "Il vous faut une clé ancienne...";
    public string texteAvecCle = "Appuyer sur [E] pour ouvrir le coffre";

    private bool estOuvert = false;

    public void Interact(GameObject interactor)
    {
        if (estOuvert) return;

        // On vérifie l'inventaire du joueur
        Inventory inventaire = interactor.GetComponent<Inventory>();

        if (inventaire != null && inventaire.HasItem(objetCleRequise))
        {
            // On retire l'objet de l'inventaire avant d'ouvrir
            inventaire.RemoveItem(objetCleRequise);

            OuvrirLeCoffre();
        }
        else
        {
            Debug.Log("Le coffre est verrouillé.");
        }
    }

    // Affiche le message quand on vise le coffre
    public string GetInteractionPrompt()
    {
        if (estOuvert) return "";

        // On cherche le joueur dans la scène
        GameObject joueur = GameObject.FindGameObjectWithTag("Player");
        
        if (joueur != null)
        {
            // On récupère son inventaire
            Inventory inventaire = joueur.GetComponent<Inventory>();
            
            // On vérifie s'il a la clé
            if (inventaire != null && !inventaire.HasItem(objetCleRequise))
            {
                // S'il n'a PAS la clé, on affiche le message d'erreur
                return texteSansCle; 
            }
        }

        // Sinon, on affiche le message normal
        return texteAvecCle;
    }

    private void OuvrirLeCoffre()
    {
        estOuvert = true;

        if (audioCoffre != null)
        {
            audioCoffre.Play();
        }

        // On lance l'animation via le paramètre "open" (Bool)
        if (animatorCoffre != null)
        {
            animatorCoffre.SetBool("open", true);
        }

        // On rend le livre visible dans le coffre
        if (livreAApparaitre != null)
        {
            livreAApparaitre.SetActive(true);
        }

        Debug.Log("Le coffre s'ouvre avec la clé !");
    }
}