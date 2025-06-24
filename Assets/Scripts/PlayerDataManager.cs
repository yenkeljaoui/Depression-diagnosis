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
