// ButtonFeedback.cs
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using static FeedbackManager;

public class ButtonFeedback : MonoBehaviour
{
    public FeedbackManager feedbackManager;
    public UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable interactable;
    public AudioSource audioSource;

    private void OnEnable()
    {
        interactable.selectEntered.AddListener(OnPressed);
    }

    private void OnDisable()
    {
        interactable.selectEntered.RemoveListener(OnPressed);
    }

    void OnHoverEnter(HoverEnterEventArgs args)
    {
        var type = feedbackManager.currentFeedbackType;
        if ((type == FeedbackType.Audio || type == FeedbackType.HapticAudioVisual) && audioSource != null)
        {
            audioSource.volume = 0.3f;
            audioSource.Play();
        }
    }

    private void OnPressed(SelectEnterEventArgs args)
    {
        var type = feedbackManager.currentFeedbackType;
        Debug.Log("Button Pressed. Feedback Type: " + type);

        if ((type == FeedbackType.Audio || type == FeedbackType.HapticAudioVisual) && audioSource != null)
        {
            Debug.Log("Playing Audio Now");
            audioSource.volume = 1.0f;
            audioSource.Play();
        }
    }

}
