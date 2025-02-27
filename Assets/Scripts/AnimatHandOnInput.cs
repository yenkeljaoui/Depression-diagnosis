using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimatHandOnInput : MonoBehaviour
{
    // Input actions for detecting finger movements
    public InputActionProperty pinchAnimationAction; // This handles the pinch (trigger) animation
    public InputActionProperty gripAnimationAction;  // This handles the grip (grab) animation

    // Reference to the hand's animator
    public Animator handAnimator;

    // Start is called before the first frame update
    void Start()
    {
        // No initialization needed in this case
    }

    // Update is called once per frame
    void Update()
    {
        // Read the trigger value (how much the player is pressing the trigger button)
        float triggerValue = pinchAnimationAction.action.ReadValue<float>();
        // Set the "Trigger" parameter in the animator, which will control the animation
        handAnimator.SetFloat("Trigger", triggerValue);

        // Read the grip value (how much the player is pressing the grip button)
        float gripValue = gripAnimationAction.action.ReadValue<float>();
        // Set the "Grip" parameter in the animator, which will control the hand's grip animation
        handAnimator.SetFloat("Grip", gripValue);
    }
}
