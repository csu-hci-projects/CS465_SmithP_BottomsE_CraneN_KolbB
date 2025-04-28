using UnityEngine;

public class FeedbackManager : MonoBehaviour
{
    public static FeedbackManager Instance { get; private set; }

    public enum FeedbackType
    {
        None,
        Visual,
        Haptic,
        Audio,
        HapticAudioVisual
    }

    [Header("Feedback Settings")]
    public FeedbackType currentFeedbackType = FeedbackType.None;

    public FeedbackType CurrentFeedbackType => currentFeedbackType;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public bool HasVisual => currentFeedbackType == FeedbackType.Visual || currentFeedbackType == FeedbackType.HapticAudioVisual;
    public bool HasHaptic => currentFeedbackType == FeedbackType.Haptic || currentFeedbackType == FeedbackType.HapticAudioVisual;
    public bool HasAudio => currentFeedbackType == FeedbackType.Audio || currentFeedbackType == FeedbackType.HapticAudioVisual;
}
