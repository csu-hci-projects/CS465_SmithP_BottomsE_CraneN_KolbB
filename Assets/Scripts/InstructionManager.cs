using System.Collections; 
using System.Collections.Generic;
using UnityEngine;

public class InstructionManager : MonoBehaviour
{
    public AudioSource audioSource;
    public List<AudioClip> audioClips;

    private List<string> instructionOrder = new List<string>();
    private int currentInstructionIndex = 0;

    public bool trialComplete { get; private set; } = false;

    public void StartInstructions(List<string> instructions)
    {
        instructionOrder = new List<string>(instructions);
        currentInstructionIndex = 0;
        trialComplete = false;
        PlayCurrentInstruction();
    }

    private void PlayCurrentInstruction()
    {
        if (currentInstructionIndex >= instructionOrder.Count)
        {
            Debug.Log("All instructions finished.");
            trialComplete = true;
            return;
        }

        string instruction = instructionOrder[currentInstructionIndex];
        AudioClip clip = FindAudioClip(instruction);

        if (clip != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
            Debug.Log("Playing instruction: " + instruction);
        }
        else
        {
            Debug.LogWarning("Audio clip not found for instruction: " + instruction);
        }
    }

    public string GetCurrentInstruction()
    {
        if (currentInstructionIndex < instructionOrder.Count)
            return instructionOrder[currentInstructionIndex];
        else
            return null;
    }

    public void RepeatCurrentInstruction()
    {
        if (currentInstructionIndex < instructionOrder.Count)
        {
            string instruction = instructionOrder[currentInstructionIndex];
            AudioClip clip = FindAudioClip(instruction);

            if (clip != null)
            {
                audioSource.Stop(); // Stop any previous playing audio
                audioSource.clip = clip;
                audioSource.Play();
                Debug.Log("Repeating instruction: " + instruction);
            }
            else
            {
                Debug.LogWarning("Audio clip not found for repeating instruction: " + instruction);
            }
        }
    }

    public void OnCorrectButtonPressed()
    {
        if (trialComplete) return;

        currentInstructionIndex++;
        PlayCurrentInstruction();
    }

    private AudioClip FindAudioClip(string instructionName)
    {
        foreach (var clip in audioClips)
        {
            if (clip.name.ToLower() == instructionName.ToLower())
            {
                return clip;
            }
        }
        return null;
    }
}
