using UnityEngine;
using System;

public class PlayerMovementTracker : MonoBehaviour
{
    // The minimal cumulative distance that counts as movement.
    [SerializeField] private float movementThreshold = 0.05f;
    // Time (in seconds) the player must be below the movement threshold before considered idle.
    [SerializeField] private float idleTimeThreshold = 5f;

    // Reference to the Main Camera (player's head movement).
    [SerializeField] private Transform cameraTransform;

    // Store the player's position from the previous frame.
    private Vector3 lastPosition;
    // Timer to track how long the player has been stationary.
    private float idleTimer = 0f;
    // Accumulates movement over frames.
    private float cumulativeDistance = 0f;
    // Current state: true if the player is idle.
    private bool isIdle = false;

    // Static event to notify subscribers (like PlayerDataManager) when idle state changes.
    // The bool parameter is true if idle, false if active.
    public static event Action<bool> OnIdleStateChanged;

    private void Start()
    {
        // Assign the Main Camera if not set in the Inspector.
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }
        // Initialize the last position with the starting camera position.
        lastPosition = cameraTransform.position;
    }

    private void Update()
    {
        // Calculate the distance moved since the last frame.
        float distance = Vector3.Distance(cameraTransform.position, lastPosition);
        // Add this frame's movement to the cumulative distance.
        cumulativeDistance += distance;

        // Check if the cumulative movement is below the defined threshold.
        if (cumulativeDistance < movementThreshold)
        {
            // Increase the idle timer by the time elapsed since the last frame.
            idleTimer += Time.deltaTime;

            // If the idle timer exceeds the threshold and the player isn't marked as idle yet,
            // trigger the idle event.
            if (idleTimer >= idleTimeThreshold && !isIdle)
            {
                isIdle = true;
                Debug.Log("Player has been idle.");
                OnIdleStateChanged?.Invoke(true);
            }
        }
        else
        {
            // If movement exceeds the threshold, reset the timer and cumulative distance.
            if (isIdle)
            {
                isIdle = false;
                Debug.Log("Player resumed moving.");
                OnIdleStateChanged?.Invoke(false);
            }
            idleTimer = 0f;
            cumulativeDistance = 0f;
        }

        // Update the last position for the next frame.
        lastPosition = cameraTransform.position;
    }
}

