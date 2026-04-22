using UnityEngine;

/// <summary>
/// À attacher sur les objets de décor que le joueur peut "regarder" sans interagir.
/// Affiche un message d'ambiance dans le HUD (ex: "Un vieux livre poussiéreux...").
/// </summary>
public class ExaminableObject : MonoBehaviour
{
    [Tooltip("Message affiché quand le joueur vise cet objet.")]
    [TextArea]
    public string examineMessage = "Cet objet ne semble pas utile...";
}
