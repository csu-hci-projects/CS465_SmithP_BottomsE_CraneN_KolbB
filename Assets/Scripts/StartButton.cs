using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    public TrialManager trialManager;
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnStartClicked);
    }

    private void OnStartClicked()
    {
        trialManager.StartTrial();  // Start the trial
        gameObject.SetActive(false); // Hide the start button after clicking
    }
}
