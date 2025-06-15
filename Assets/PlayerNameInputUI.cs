// // using UnityEngine;
// // using TMPro;
// // using UnityEngine.UI;

// // public class PlayerNameInputUI : MonoBehaviour
// // {
// //     public TMP_InputField nameInputField;    // Reference to the input field for entering the player's name
// //     public Button startGameButton;           // Button that starts the game after name entry
// //     public PlayerDataManager playerDataManager; // Reference to the PlayerDataManager for saving the name
// //     public GameObject thisCanvasToDisable;    // The canvas to hide once the name has been entered

// //     void Start()
// //     {
// //         // Add a listener to call OnStartGameClicked when the button is pressed
// //         startGameButton.onClick.AddListener(OnStartGameClicked);
// //     }

// //     void OnStartGameClicked()
// //     {
// //         // Trim whitespace and get the entered player name
// //         string playerName = nameInputField.text.Trim();

// //         // If the name is empty, warn the developer and do nothing
// //         if (string.IsNullOrEmpty(playerName))
// //         {
// //             Debug.LogWarning("Player name is empty.");
// //             return;
// //         }

// //         // Save the player's name via the PlayerDataManager
// //         playerDataManager.SetPlayerName(playerName);

// //         // Hide this canvas to proceed to the next UI step
// //         if (thisCanvasToDisable != null)
// //             thisCanvasToDisable.SetActive(false);
// //     }
// // }



// using UnityEngine;
// using TMPro;
// using UnityEngine.UI;
// using Microsoft.MixedReality.Toolkit.Experimental.UI;


// public class PlayerNameInputUI : MonoBehaviour
// {
    
//     [Header("UI References")]
//     public TMP_InputField nameInputField;           // Reference to the name input field
//     public Button startGameButton;                  // Button to proceed after name entry
//     public GameObject virtualKeyboard;              // Virtual keyboard to show on input select
//     public GameObject thisCanvasToDisable;          // Canvas to hide after entering name
    
    

//     [Header("Managers")]
//     public PlayerDataManager playerDataManager;     // Handles saving the player name

//     void Start()
//     {
//         // When the input field is selected, open the virtual keyboard
//         nameInputField.onSelect.AddListener(OnInputFieldSelected);

//         // When the Start Game button is clicked, save name and continue
//         startGameButton.onClick.AddListener(OnStartGameClicked);

      
//     }


//     void OnKeyboardTextSubmitted(object sender, EventArgs e)
//     {
//         // קח את הטקסט מהמקלדת
//         string enteredText = NonNativeKeyboard.Instance.InputField.text;

//         // הצג אותו בשדה שלך
//         nameInputField.text = enteredText;
//     }

//     void OnInputFieldSelected(string text)
//     {
//         // Show the virtual keyboard when input field is selected
//         if (virtualKeyboard != null)
//             virtualKeyboard.SetActive(true);
//     }

//     void OnStartGameClicked()
//     {
//         // Get trimmed player name
//         string playerName = nameInputField.text.Trim();

//         // Prevent empty names
//         if (string.IsNullOrEmpty(playerName))
//         {
//             Debug.LogWarning("Player name is empty.");
//             return;
//         }

//         // Save the player's name
//         playerDataManager.SetPlayerName(playerName);

//         // Hide the virtual keyboard
//         if (virtualKeyboard != null)
//             virtualKeyboard.SetActive(false);

//         // Hide the input UI canvas
//         if (thisCanvasToDisable != null)
//             thisCanvasToDisable.SetActive(false);
//     }
// }




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
