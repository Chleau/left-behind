using UnityEngine;
using TMPro;

[RequireComponent(typeof(AudioSource))]
public class CompteARebours : MonoBehaviour
{
    public float tempsInitial = 60f;
    private float tempsRestant;
    public TextMeshProUGUI texteAffichage;
    public GameObject ecranMort;

    [Header("Audio")]
    [SerializeField] private AudioClip sonMort;

    private AudioSource _audioSource;
    private bool estFini = false;
    private bool chronoLance = false;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        tempsRestant = tempsInitial;
        MettreAJourUI();
    }

    public void LancerChrono()
    {
        chronoLance = true;
    }

    void Update()
    {
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

        _audioSource.Stop();
        if (sonMort != null) _audioSource.PlayOneShot(sonMort);

        if (ecranMort != null) ecranMort.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
    }
}
