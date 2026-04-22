using UnityEngine;
using UnityEngine.SceneManagement; 

public class LancerJeu : MonoBehaviour
{
    public GameObject menuComplet;
    public CompteARebours scriptChrono;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Demarrer()
    {
        if (menuComplet != null)
        {
            menuComplet.SetActive(false);
        }

        if (scriptChrono != null)
        {
            scriptChrono.LancerChrono();
        }

        // QUAND ON JOUE : On verrouille la souris pour diriger le perso
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        Time.timeScale = 1f;
    }

    public void Rejouer()
    {
        // Cette ligne recharge la scène active (celle où tu es)
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
        // On s'assure que le temps reprend (au cas où il était en pause)
        Time.timeScale = 1f;
    }
}