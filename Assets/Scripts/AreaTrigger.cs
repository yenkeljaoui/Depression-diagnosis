// using UnityEngine;

// public class AreaTrigger : MonoBehaviour
// {
//     [Header("Area Settings")]
//     [SerializeField] private string areaName = "LakeArea";

//     private int timesEntered = 0;
//     private int timesExited = 0;
//     private float enterTime = 0f;
//     private float totalTimeSpent = 0f;

//     private PlayerDataManager dataManager;
//     private bool hasEntered = false;
//     private float lastEnterTime = -1f; // זמן כניסה אחרון
//     private float minEnterDelay = 0.5f; // זמן מינימלי בין כניסות (למניעת כפילות)

//     private void Start()
//     {
//         dataManager = FindFirstObjectByType<PlayerDataManager>();

//         if (dataManager == null)
//         {
//             Debug.LogError("PlayerDataManager not found in the scene!");
//         }
//     }

//     private void OnTriggerEnter(Collider other)
//     {
//         Debug.Log($"Trigger activated by: {other.gameObject.name}");

//         if (other.CompareTag("Player") && other.gameObject.name == "XR Origin (XR Rig)")
//         {
//             float currentTime = Time.time;
//             if (!hasEntered && (lastEnterTime < 0 || (currentTime - lastEnterTime) > minEnterDelay))
//             {
//                 hasEntered = true;
//                 lastEnterTime = currentTime; // עדכון זמן הכניסה האחרון

//                 timesEntered++;
//                 enterTime = Time.time;

//                 if (dataManager != null)
//                 {
//                     dataManager.LogEvent(
//                         "AreaEnter",
//                         $"Player entered {areaName} | timesEntered = {timesEntered} | time = {Time.time}"
//                     );
//                 }

//                 Debug.Log($"✅ [AreaTrigger] Player ENTERED {areaName}. Times Entered: {timesEntered}");
//             }
//             else
//             {
//                 Debug.Log($"⚠️ [AreaTrigger] Ignored duplicate entry for {areaName}.");
//             }
//         }
//     }

//     private void OnTriggerExit(Collider other)
//     {
//         if (other.CompareTag("Player") && other.gameObject.name == "XR Origin (XR Rig)")
//         {
//             hasEntered = false;
//             timesExited++;
//             float sessionTime = Time.time - enterTime;
//             totalTimeSpent += sessionTime;

//             if (dataManager != null)
//             {
//                 dataManager.LogEvent(
//                     "AreaExit",
//                     $"Player left {areaName} | timesExited = {timesExited} | " +
//                     $"sessionTime = {sessionTime:F2}s | totalTimeSpent = {totalTimeSpent:F2}s | time = {Time.time}"
//                 );
//             }

//             Debug.Log($"✅ [AreaTrigger] Player LEFT {areaName}. Session Time: {sessionTime:F2}s, Total Time Spent: {totalTimeSpent:F2}s");
//         }
//     }
// }



using UnityEngine;

public class AreaTrigger : MonoBehaviour
{
    [Header("Area Settings")]
    [SerializeField] private string areaName = "LakeArea"; // The name of the area, set in the Inspector

    private int timesEntered = 0; // Counter for the number of times the player enters the area
    private int timesExited = 0;  // Counter for the number of times the player exits the area
    private float enterTime = 0f; // The timestamp of the last entry
    private float totalTimeSpent = 0f; // Accumulates the total time spent inside the area

    private PlayerDataManager dataManager; // Reference to the PlayerDataManager
    private bool hasEntered = false; // Flag to prevent duplicate entries
    private float lastEnterTime = -1f; // Stores the last time the player entered
    private float minEnterDelay = 0.5f; // Minimum delay between valid entries to prevent duplicates
    private float lastExitTime = -1f;  // Last exit timestamp
    private float minExitDelay = 0.5f;  // Minimum time between exits

    private void Start()
    {
        // Locate the PlayerDataManager in the scene
        dataManager = FindFirstObjectByType<PlayerDataManager>();

        // Log an error if the PlayerDataManager is not found
        if (dataManager == null)
        {
            Debug.LogError("PlayerDataManager not found in the scene!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Ensure only the XR Origin (main player body) triggers the event
        if (other.CompareTag("Player") && other.transform.root.gameObject.name == "XR Origin (XR Rig)")
        // if (other.CompareTag("Player") && other.gameObject.name == "XR Origin (XR Rig)")
        {
            float currentTime = Time.time;

            // Allow entry only if enough time has passed since the last entry
            if (!hasEntered && (lastEnterTime < 0 || (currentTime - lastEnterTime) > minEnterDelay))
            {
                hasEntered = true; // Mark that the player has entered
                lastEnterTime = currentTime; // Update the last entry timestamp

                timesEntered++; // Increment entry count
                enterTime = Time.time; // Store the current entry time

                // Log the entry event in the PlayerDataManager
                if (dataManager != null)
                {
                    dataManager.LogEvent(
                        "AreaEnter",
                        $"Player entered {areaName} | timesEntered = {timesEntered} | time = {Time.time}"
                    );
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
{
    if (other.CompareTag("Player") && other.transform.root.gameObject.name == "XR Origin (XR Rig)")
    {
        float currentTime = Time.time;

        // Ensure enough time has passed since the last exit to avoid duplicate triggers
        if (lastExitTime < 0 || (currentTime - lastExitTime) > minExitDelay)
        {
            hasEntered = false; // Reset entry flag
            lastExitTime = currentTime; // Update last exit time
            timesExited++; // Increment exit count

            float sessionTime = Time.time - enterTime; // Calculate time spent in the area
            totalTimeSpent += sessionTime; // Update total time spent

            // Log exit event in PlayerDataManager
            if (dataManager != null)
            {
                dataManager.LogEvent(
                    "AreaExit",
                    $"Player left {areaName} | timesExited = {timesExited} | " +
                    $"sessionTime = {sessionTime:F2}s | totalTimeSpent = {totalTimeSpent:F2}s | time = {Time.time}"
                );
            }
        }
    }
}
}
