using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class PlayerDataManager : MonoBehaviour
{
    // Path to save the data file (e.g., "PlayerData.json")
    private string filePath;

    // Data structure to hold session data (list of events)
    public PlayerSessionData sessionData;

    void Awake()
    {
        // Set the file path to a persistent data path that is the same across sessions
        filePath = Path.Combine(Application.persistentDataPath, "PlayerData.json");
        sessionData = new PlayerSessionData();
    }

    // Method to log an event; this can be called by other components, the func will call by the interactable object
    public void LogEvent(string eventName, string details)
    {
        PlayerEvent newEvent = new PlayerEvent()
        {
            eventName = eventName,
            details = details,
            timeStamp = Time.time  // Record the time since the game started
        };

        sessionData.events.Add(newEvent);
        Debug.Log("Logged event: " + eventName + " - " + details);
    }

    // Save the data to the file when the application quits
    void OnApplicationQuit()
    {
        SaveData();
    }

    // Writes the session data as JSON to a file, overwriting previous data
    public void SaveData()
    {
        string json = JsonUtility.ToJson(sessionData, true);
        File.WriteAllText(filePath, json);
        Debug.Log("Data saved to " + filePath);
    }
}

// Data structure for the entire session
[System.Serializable]
public class PlayerSessionData
{
    public List<PlayerEvent> events = new List<PlayerEvent>();
}

// Data structure for each event
[System.Serializable]
public class PlayerEvent
{
    public string eventName;   // e.g., "FollowInstruction", "InteractObject"
    public string details;     // Extra information about the event (object type, etc.)
    public float timeStamp;    // When the event happened
}
