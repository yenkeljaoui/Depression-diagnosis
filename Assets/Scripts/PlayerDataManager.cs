    // using UnityEngine;
    // using System.IO;
    // using System.Collections.Generic;

    // public class PlayerDataManager : MonoBehaviour
    // {
    //     // Path where the data file is saved.
    //     private string filePath;
    //     // Structure that holds all the session's events.
    //     public PlayerSessionData sessionData;

    //     private void Awake()
    //     {
    //         // Build the file path using Unity's persistent data path.
    //         filePath = Path.Combine(Application.persistentDataPath, "PlayerData.json");
    //         sessionData = new PlayerSessionData();
    //     }

    //     // Subscribe to the OnIdleStateChanged event from PlayerMovementTracker.
    //     private void OnEnable()
    //     {
    //         PlayerMovementTracker.OnIdleStateChanged += HandleIdleStateChanged;
    //     }

    //     // Unsubscribe when this object is disabled to avoid memory leaks.
    //     private void OnDisable()
    //     {
    //         PlayerMovementTracker.OnIdleStateChanged -= HandleIdleStateChanged;
    //     }

    //     // Event handler that logs idle or active events.
    //     private void HandleIdleStateChanged(bool isIdle)
    //     {
    //         // If the player is idle, log a "PlayerIdle" event.
    //         if (isIdle)
    //         {
    //             LogEvent("PlayerIdle", "Player became idle at time: " + Time.time);
    //         }
    //         // If the player resumes moving, log a "PlayerActive" event.
    //         else
    //         {
    //             LogEvent("PlayerActive", "Player resumed movement at time: " + Time.time);
    //         }
    //     }

    //     // Log an event; this method is also used by other interactable components.
    //     public void LogEvent(string eventName, string details)
    //     {
    //         PlayerEvent newEvent = new PlayerEvent()
    //         {
    //             eventName = eventName,
    //             details = details,
    //             timeStamp = Time.time  // Using the time since the game started.
    //         };

    //         sessionData.events.Add(newEvent);
    //         //Debug.Log("Logged event: " + eventName + " - " + details);
    //     }

    //     // Save data when the application quits.
    //     private void OnApplicationQuit()
    //     {
    //         SaveData();
    //     }

    //     // Save the session data as JSON to a file, overwriting any previous data.
    //     public void SaveData()
    //     {
    //         string json = JsonUtility.ToJson(sessionData, true);
    //         File.WriteAllText(filePath, json);
    //         Debug.Log("Data saved to " + filePath);
    //     }
    // }

    // // Data structure for the entire session, including all logged events.
    // [System.Serializable]
    // public class PlayerSessionData
    // {
    //     public List<PlayerEvent> events = new List<PlayerEvent>();
    // }

    // // Data structure representing an individual event.
    // [System.Serializable]
    // public class PlayerEvent
    // {
    //     public string eventName;   // e.g., "PlayerIdle", "PlayerActive", "InteractObject"
    //     public string details;     // Extra details about the event (e.g., which object was touched)
    //     public float timeStamp;    // Timestamp of when the event occurred
    // }





using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class PlayerDataManager : MonoBehaviour
{
    private string filePath;
    public PlayerSessionData sessionData;
    public string playerName;

    private void Awake()
    {
        sessionData = new PlayerSessionData();
    }

    public void SetPlayerName(string name)
    {
        playerName = name;
        filePath = Path.Combine(Application.persistentDataPath, $"{playerName}_PlayerData.json");
        Debug.Log("Player file path: " + filePath);
    }

    private void OnEnable()
    {
        PlayerMovementTracker.OnIdleStateChanged += HandleIdleStateChanged;
    }

    private void OnDisable()
    {
        PlayerMovementTracker.OnIdleStateChanged -= HandleIdleStateChanged;
    }

    private void HandleIdleStateChanged(bool isIdle)
    {
        if (isIdle)
            LogEvent("PlayerIdle", "Player became idle at time: " + Time.time);
        else
            LogEvent("PlayerActive", "Player resumed movement at time: " + Time.time);
    }

    public void LogEvent(string eventName, string details)
    {
        PlayerEvent newEvent = new PlayerEvent()
        {
            eventName = eventName,
            details = details,
            timeStamp = Time.time
        };

        sessionData.events.Add(newEvent);
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }

    public void SaveData()
    {
        if (string.IsNullOrEmpty(filePath))
        {
            Debug.LogError("No player name set. Cannot save data.");
            return;
        }

        string json = JsonUtility.ToJson(sessionData, true);
        File.WriteAllText(filePath, json);
        Debug.Log("Data saved to " + filePath);
    }
}

[System.Serializable]
public class PlayerSessionData
{
    public List<PlayerEvent> events = new List<PlayerEvent>();
}

[System.Serializable]
public class PlayerEvent
{
    public string eventName;
    public string details;
    public float timeStamp;
}
