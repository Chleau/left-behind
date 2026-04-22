using UnityEngine;
using TMPro;

public class CompteARebours : MonoBehaviour
{
    public float tempsInitial = 60f;
    private float tempsRestant;
    public TextMeshProUGUI texteAffichage;
    public GameObject ecranMort;
    
    private bool estFini = false;
    private bool chronoLance = false; // Le chrono est arrêté par défaut

    void Start()
    {
        tempsRestant = tempsInitial;
        MettreAJourUI(); // Affiche le temps initial (ex: 01:00)
    }

    // CETTE FONCTION SERA APPELÉE PAR LE BOUTON COMMENCER
    public void LancerChrono()
    {
        chronoLance = true;
    }

    void Update()
    {
        // On ne fait rien tant que le chrono n'est pas lancé ou s'il est fini
        if (!chronoLance || estFini) return;

        if (tempsRestant > 0)
        {
            tempsRestant -= Time.deltaTime;
            MettreAJourUI();
        }
        else
        {
            TerminerLaPartie();
        }
    }

    void MettreAJourUI()
    {
        int minutes = Mathf.FloorToInt(tempsRestant / 60);
        int secondes = Mathf.FloorToInt(tempsRestant % 60);
        texteAffichage.text = string.Format("{0:00}:{1:00}", minutes, secondes);
    }

    void TerminerLaPartie()
    {
        estFini = true;
        tempsRestant = 0;
        if (texteAffichage != null) texteAffichage.text = "00:00";
        if (ecranMort != null) ecranMort.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
    }
}