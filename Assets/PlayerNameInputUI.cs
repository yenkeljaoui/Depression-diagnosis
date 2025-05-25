using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerNameInputUI : MonoBehaviour
{
    public TMP_InputField nameInputField;    // Reference to the input field for entering the player's name
    public Button startGameButton;           // Button that starts the game after name entry
    public PlayerDataManager playerDataManager; // Reference to the PlayerDataManager for saving the name
    public GameObject thisCanvasToDisable;    // The canvas to hide once the name has been entered

    void Start()
    {
        // Add a listener to call OnStartGameClicked when the button is pressed
        startGameButton.onClick.AddListener(OnStartGameClicked);
    }

    void OnStartGameClicked()
    {
        // Trim whitespace and get the entered player name
        string playerName = nameInputField.text.Trim();

        // If the name is empty, warn the developer and do nothing
        if (string.IsNullOrEmpty(playerName))
        {
            Debug.LogWarning("Player name is empty.");
            return;
        }

        // Save the player's name via the PlayerDataManager
        playerDataManager.SetPlayerName(playerName);

        // Hide this canvas to proceed to the next UI step
        if (thisCanvasToDisable != null)
            thisCanvasToDisable.SetActive(false);
    }
}
