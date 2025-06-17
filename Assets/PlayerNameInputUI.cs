
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System; // ✅ נדרש עבור EventArgs
using Microsoft.MixedReality.Toolkit.Experimental.UI;

public class PlayerNameInputUI : MonoBehaviour
{
    [Header("UI References")]
    public TMP_InputField nameInputField;           // Reference to the name input field
    public Button startGameButton;                  // Button to proceed after name entry
    public GameObject virtualKeyboard;              // Virtual keyboard to show on input select
    public GameObject thisCanvasToDisable;          // Canvas to hide after entering name

    [Header("Managers")]
    public PlayerDataManager playerDataManager;     // Handles saving the player name

    void Start()
    {
        // Show the keyboard when the input field is selected
        nameInputField.onSelect.AddListener(OnInputFieldSelected);

        // Save the name and proceed when the button is clicked
        startGameButton.onClick.AddListener(OnStartGameClicked);

        // ✅ Listen for text submission from the virtual keyboard
        NonNativeKeyboard.Instance.OnTextSubmitted += OnKeyboardTextSubmitted;
    }

    // Triggered when the input field is selected (clicked/tapped)
    void OnInputFieldSelected(string text)
    {
        if (virtualKeyboard != null)
        {
            virtualKeyboard.SetActive(true);
            NonNativeKeyboard.Instance.PresentKeyboard(); // ✅ Show the keyboard interface
        }
    }

    // Triggered when 'Enter' is pressed on the virtual keyboard
    void OnKeyboardTextSubmitted(object sender, EventArgs e)
    {
        string enteredText = NonNativeKeyboard.Instance.InputField.text;
        nameInputField.text = enteredText;

        // Hide the keyboard after submission
        if (virtualKeyboard != null)
            virtualKeyboard.SetActive(false);
    }

    // Triggered when the Start Game button is pressed
    void OnStartGameClicked()
    {
        string playerName = nameInputField.text.Trim();

        if (string.IsNullOrEmpty(playerName))
        {
            Debug.LogWarning("Player name is empty.");
            return;
        }

        playerDataManager.SetPlayerName(playerName);

        if (virtualKeyboard != null)
            virtualKeyboard.SetActive(false);

        if (thisCanvasToDisable != null)
            thisCanvasToDisable.SetActive(false);
    }
}
