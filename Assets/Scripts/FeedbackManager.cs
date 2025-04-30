using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Feedback;

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
    public SimpleHapticFeedback simpleHaptic;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void OnEnable()
    {
        UpdateHaptics();
    }

    private void Update()
    {
        UpdateHaptics();
    }

    private void UpdateHaptics()
    {
        if (simpleHaptic != null)
        {
            simpleHaptic.enabled = currentFeedbackType == FeedbackType.Haptic || currentFeedbackType == FeedbackType.HapticAudioVisual;
        }
    }

    public bool HasVisual => currentFeedbackType == FeedbackType.Visual || currentFeedbackType == FeedbackType.HapticAudioVisual;
    public bool HasHaptic => currentFeedbackType == FeedbackType.Haptic || currentFeedbackType == FeedbackType.HapticAudioVisual;
    public bool HasAudio => currentFeedbackType == FeedbackType.Audio || currentFeedbackType == FeedbackType.HapticAudioVisual;
}
