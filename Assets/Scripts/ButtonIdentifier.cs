using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System;

public class ButtonIdentifier : MonoBehaviour
{
    public string buttonName;
    public static string LastPressed = "";
    public static float lastHoverStartTime = 0f;

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable interactable;

    private void Awake()
    {
        interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable>();

        if (interactable != null)
        {
            interactable.hoverEntered.AddListener(OnHoverEnter);
            interactable.selectEntered.AddListener(OnSelectEntered);
        }
    }

    private void OnHoverEnter(HoverEnterEventArgs args)
    {
        lastHoverStartTime = Time.time;
    }

    public static float GetHoverDurationAndReset()
    {
        if (lastHoverStartTime == 0f)
            return 0f;

        float duration = Time.time - lastHoverStartTime;
        lastHoverStartTime = 0f;
        return duration;
    }
    public static bool ButtonWasPressedThisFrame = false;

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        LastPressed = buttonName.ToLower();
        ButtonWasPressedThisFrame = true;
        Debug.Log($"Button pressed: {LastPressed}");
    }
}
