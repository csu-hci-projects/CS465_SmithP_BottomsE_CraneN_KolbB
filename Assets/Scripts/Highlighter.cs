using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Highlighter : MonoBehaviour
{
    public Material normalMaterial;
    public Material highlightMaterial;
    public AudioSource audioSource;
    public FeedbackManager feedbackManager;

    private MeshRenderer meshRenderer;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = normalMaterial;

        var interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable>();
        interactable.hoverEntered.AddListener(OnHoverEnter);
        interactable.hoverExited.AddListener(OnHoverExit);
    }

    void OnHoverEnter(HoverEnterEventArgs args)
    {
        if (FeedbackManager.Instance.HasVisual)
            meshRenderer.material = highlightMaterial;
    }

    void OnHoverExit(HoverExitEventArgs args)
    {
        meshRenderer.material = normalMaterial;
    }
}
