using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Haptics;

public class Highlighter : MonoBehaviour
{
    public Material normalMaterial;
    public Material highlightMaterial;
    public AudioSource audioSource;

    private MeshRenderer meshRenderer;
    private MxInkHandler stylusHandler;
    private HapticImpulsePlayer hapticPlayer;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = normalMaterial;

        var interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable>();
        interactable.hoverEntered.AddListener(OnHoverEnter);
        interactable.hoverExited.AddListener(OnHoverExit);
        interactable.selectEntered.AddListener(OnClick);

        stylusHandler = FindObjectOfType<MxInkHandler>();
        hapticPlayer = FindFirstObjectByType<HapticImpulsePlayer>(); // get the Near-Far Interactor's haptic
    }

    void OnHoverEnter(HoverEnterEventArgs args)
    {
        if (FeedbackManager.Instance.HasVisual)
            meshRenderer.material = highlightMaterial;

        if (FeedbackManager.Instance.HasHaptic && hapticPlayer != null)
            hapticPlayer.SendHapticImpulse(0.2f, 0.05f);
    }

    void OnHoverExit(HoverExitEventArgs args)
    {
        meshRenderer.material = normalMaterial;
    }

    void OnClick(SelectEnterEventArgs args)
    {
        if (FeedbackManager.Instance.HasAudio && audioSource != null)
            audioSource.Play();

        if (FeedbackManager.Instance.HasHaptic && hapticPlayer != null)
            hapticPlayer.SendHapticImpulse(1.0f, 0.1f);
    }
}
