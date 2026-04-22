using UnityEngine;

public class LancerJeu : MonoBehaviour
{
    public GameObject menuComplet;

    void Start()
    {
        // AU DÉBUT : On libère la souris pour pouvoir cliquer sur le menu
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Demarrer()
    {
        if (menuComplet != null)
        {
            menuComplet.SetActive(false);
        }

        // QUAND ON JOUE : On verrouille la souris pour diriger le perso
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        Time.timeScale = 1f;
    }
}