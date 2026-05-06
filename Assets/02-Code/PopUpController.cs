using UnityEngine;

public class PopUpController : MonoBehaviour
{
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        // On s'assure que le panel est invisible au début
        // via le Canvas Group ou en désactivant l'objet
    }

    public void DeclencherPopUp()
    {
        // On active l'objet s'il était éteint
        gameObject.SetActive(true);
        
        // On joue l'animation de FadeIn
        anim.Play("FadeInPopUp");
        
        // Optionnel : On le cache après 2 secondes
        Invoke("Cacher", 2f);
    }

    void Cacher()
    {
        gameObject.SetActive(false);
    }
}