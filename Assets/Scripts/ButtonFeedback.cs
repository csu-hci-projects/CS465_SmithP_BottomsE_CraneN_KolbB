using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using static FeedbackManager;

public class ButtonFeedback : MonoBehaviour
{
    public FeedbackManager feedbackManager;
    public UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable interactable;
    public AudioSource audioSource; // optional - for click sound

    private void OnEnable()
    {
        interactable.selectEntered.AddListener(OnPressed);
    }

    private void OnDisable()
    {
        interactable.selectEntered.RemoveListener(OnPressed);
    }

    private void OnPressed(SelectEnterEventArgs args)
    {
        var type = feedbackManager.currentFeedbackType;

        if (type == FeedbackManager.FeedbackType.Haptic || type == FeedbackManager.FeedbackType.HapticAudioVisual)
            SendHaptic(args.interactorObject);

        if (type == FeedbackManager.FeedbackType.Audio || type == FeedbackManager.FeedbackType.HapticAudioVisual)
            PlaySound();
    }

    private void SendHaptic(UnityEngine.XR.Interaction.Toolkit.Interactors.IXRInteractor interactor)
    {
        if (interactor is UnityEngine.XR.Interaction.Toolkit.Interactors.XRBaseInputInteractor controllerInteractor)
        {
            controllerInteractor.SendHapticImpulse(0.5f, 0.1f); // strength, duration
        }
    }

    private void PlaySound()
    {
        if (audioSource != null)
            audioSource.Play();
    }
}
