using System.Collections.Generic;
using UnityEngine;

public class TrialManager : MonoBehaviour
{
    public InstructionManager instructionManager;
    public DataLogger dataLogger;
    public TextAsset permutationCSV;
    public FeedbackManager feedbackManager;
    public string participantID = "1";

    private List<List<string>> permutations = new List<List<string>>();
    private List<string> currentPermutation = new List<string>();
    private int currentInstructionIndex = 0;

    private bool trialRunning = false;

    private void Start()
    {
        LoadPermutationsFromCSV();
    }

    private void LoadPermutationsFromCSV()
    {
        permutations.Clear();
        string[] lines = permutationCSV.text.Split('\n');
        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            string[] words = line.Trim().Split(',');
            permutations.Add(new List<string>(words));
        }
    }

    public void StartTrial()
    {
        if (permutations.Count == 0)
        {
            Debug.LogError("No permutations loaded!");
            return;
        }

        // Pick a random permutation
        int randomIndex = Random.Range(0, permutations.Count);
        currentPermutation = permutations[randomIndex];

        currentInstructionIndex = 0;

        List<string> selectedInstructions = new List<string>();
        while (selectedInstructions.Count < 50)
        {
            selectedInstructions.Add(currentPermutation[Random.Range(0, currentPermutation.Count)]);
        }

        instructionManager.StartInstructions(selectedInstructions);
        dataLogger.StartTrial();
        trialRunning = true;

    }

    private void Update()
    {
        if (!trialRunning)
            return;

        if (instructionManager.trialComplete)
        {
            EndTrial();
            return;
        }

        string expectedButton = instructionManager.GetCurrentInstruction();
        string lastPressed = ButtonIdentifier.LastPressed;

        if (!string.IsNullOrEmpty(lastPressed))
        {
            if (!string.IsNullOrEmpty(expectedButton) && expectedButton.ToLower() == lastPressed)
            {
                Debug.Log("Correct button pressed: " + lastPressed);
                float hoverToClickTime = ButtonIdentifier.GetHoverDurationAndReset();
                dataLogger.RecordInstruction(true);
                dataLogger.RecordSelection(true, hoverToClickTime);

                ButtonIdentifier.LastPressed = ""; // reset
                instructionManager.OnCorrectButtonPressed(); // move to next instruction
            }
            else
            {
                Debug.LogWarning("Wrong button pressed: " + lastPressed);
                dataLogger.RecordInstruction(false);
                dataLogger.RecordSelection(false, 0f);

                ButtonIdentifier.LastPressed = ""; // reset

                //replay the same instruction audio if wrong
                instructionManager.RepeatCurrentInstruction();
            }
        }
    }





    public void OnInstructionCompleted(bool correct, float hoverToClickTime)
    {
        if (!trialRunning) return;

        dataLogger.RecordInstruction(correct);
        dataLogger.RecordSelection(correct, hoverToClickTime);

        currentInstructionIndex++;

        if (currentInstructionIndex >= currentPermutation.Count)
        {
            EndTrial();
        }
    }

    private void EndTrial()
    {
        trialRunning = false;

        string feedbackName = feedbackManager.CurrentFeedbackType.ToString();

        // Example: "P1_Haptic_trial.txt"
        string fileName = $"P{participantID}_{feedbackName}_trial.txt";

        Debug.Log($"Trial ended. Saving to {fileName}");

        dataLogger.EndTrial(participantID, feedbackName, fileName);
    }
}
