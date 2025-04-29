using System.IO;
using UnityEngine;
using System.Collections.Generic;

public class DataLogger : MonoBehaviour
{
    private int correctCount = 0;
    private int instructionCount = 0;
    private int hitCount = 0;
    private int clickCount = 0;
    private float totalSelectionSpeed = 0f;
    private float trialStartTime = 0f;
    private string filePath;

    public void StartTrial()
    {
        correctCount = 0;
        instructionCount = 0;
        hitCount = 0;
        clickCount = 0;
        totalSelectionSpeed = 0f;
        trialStartTime = Time.time;
    }

    public void RecordInstruction(bool correct)
    {
        instructionCount++;
        if (correct)
            correctCount++;
    }

    public void RecordSelection(bool hit, float hoverToClickTime)
    {
        clickCount++;
        if (hit)
        {
            hitCount++;
            totalSelectionSpeed += hoverToClickTime;
        }
    }

    public void EndTrial(string participantID, string feedbackType, string fileName)
    {
        if (!Directory.Exists(Application.dataPath + "/DataLogs"))
        {
            Directory.CreateDirectory(Application.dataPath + "/DataLogs");
        }

        filePath = Path.Combine(Application.dataPath, "DataLogs", fileName);

        float trialCompletionTime = Time.time - trialStartTime;
        float correctnessRate = instructionCount > 0 ? (float)correctCount / instructionCount : 0f;
        float hitRate = clickCount > 0 ? (float)hitCount / clickCount : 0f;
        float meanSelectionSpeed = hitCount > 0 ? totalSelectionSpeed / hitCount : 0f;

        using (StreamWriter writer = new StreamWriter(filePath))
        {
            writer.WriteLine($"Trial Completion Time (seconds): {trialCompletionTime:F1}");
            writer.WriteLine($"Correctness Rate (%): {correctnessRate:F2}");
            writer.WriteLine($"Hit Rate (%): {hitRate:F2}");
            writer.WriteLine($"Mean Selection Speed (seconds): {meanSelectionSpeed:F2}");
        }

        Debug.Log("Data written to: " + filePath);
    }
}
