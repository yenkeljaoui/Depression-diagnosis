using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class GameMenuUiScenario1 : MonoBehaviour
{
    public PerformanceLoggerScenario1 performanceLogger;

    // -- Player name input UI ----------------------------------
    public GameObject nameInputCanvas;            // Canvas for entering the player's name
    public PlayerNameInputUI nameInputUI;         // Reference to the PlayerNameInputUI script
    private bool nameEntered = false;             // Flag to track when the name has been entered

    // -- Head-tracking reference ------------------------------
    public Transform head;                        // XR camera (player's head) transform

    // -- UI panels ---------------------------------------------
    public GameObject tiger;                      // The tiger GameObject to activate in Scenario 2
    public GameObject menu;                       // Main UI panel (Game Menu)
    public GameObject timeSelectionCanvas;        // The Time Selection UI panel

    // -- UI buttons --------------------------------------------
    public Button continueButton;                 // Main "Continue" button for tutorial steps
    public Button noonButton, eveningButton, nightButton; // Buttons for choosing time of day
    public Button timeContinueButton;             // "Continue" button inside the time-selection canvas

    // -- UI text elements --------------------------------------
    public TextMeshProUGUI instructionText;       // Instructional text shown to the player
    public TextMeshProUGUI timerText;             // UI text showing the countdown timer

    // -- Internal state ----------------------------------------
    private int step = 0;                         // Tracks which tutorial step we're on
    private bool timerActive = false;             // Whether the countdown timer is running
    private float timeRemaining = 60f;             // Duration of the countdown in seconds
    private bool timeSelected = false;            // Whether the player has picked a time

   public void Start()
    {
        performanceLogger = GetComponent<PerformanceLoggerScenario1>();

        // 1) Show name input canvas first; hide everything else until a name is entered
        nameInputCanvas.SetActive(true);
        menu.SetActive(false);
        timeSelectionCanvas.SetActive(false);

        // 2) Register callback for when the player clicks "Start" on the name-input canvas
        nameInputUI.startGameButton.onClick.AddListener(OnNameEntered);

        // 3) Set up tutorial continue button
        if (continueButton != null)
            continueButton.onClick.AddListener(NextStep);
        else
            Debug.LogError("Continue Button is not assigned!");

        // 4) Hide timer UI at the beginning
        timerText.gameObject.SetActive(false);

        // 5) Register time-selection buttons
        if (noonButton != null)    noonButton.onClick.AddListener(() => SelectTime("Noon"));
        if (eveningButton != null) eveningButton.onClick.AddListener(() => SelectTime("Evening"));
        if (nightButton != null)   nightButton.onClick.AddListener(() => SelectTime("Night"));

        // 6) Register the Continue button inside the time-selection canvas
        if (timeContinueButton != null)
            timeContinueButton.onClick.AddListener(StartTimer);
        else
            Debug.LogError("Time Continue Button is not assigned!");
    }

    void OnNameEntered()
    {
        // Called when the player has entered their name and clicked Start
        nameEntered = true;
        nameInputCanvas.SetActive(false);       // Hide the name-input UI
        menu.SetActive(true);                   // Show the main menu UI
        continueButton.gameObject.SetActive(true); 
        // Optionally show a welcome message:
        instructionText.text = "Welcome, " + nameInputUI.nameInputField.text + "! Let's begin.";
    }

   public void Update()
    {
        // If the countdown timer is active, update it each frame
        if (timerActive && timeSelected)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimerUI();
            }
            else
            {
                // When time runs out, transition to Scenario 2
                timerActive = false;
                timeRemaining = 0;
                ShowScenario2Message();
            }
        }
    }

    void NextStep()
    {
        // Advance to the next tutorial step
        step++;

        switch (step)
        {
            case 1:
                instructionText.text = "To move, use the left joystick.";
                break;
            case 2:
                instructionText.text = "To grab an object, press the grip button.";
                break;
            case 3:
                instructionText.text = "All set, enjoy the  adventure..";
                continueButton.gameObject.SetActive(false); // Hide the tutorial button
                timeSelectionCanvas.SetActive(true);        // Show time-selection UI
                menu.SetActive(false);                      // Hide main menu
                break;
        }
    }

    void SelectTime(string timeOfDay)
    {
        // Apply lighting settings based on the chosen time
        switch (timeOfDay)
        {
            case "Noon":
                UpdateLighting(12f, 7000f, 5.0f, 1.5f);
                break;
            case "Evening":
                UpdateLighting(18f, 3500f, 1.5f, 1.0f);
                break;
            case "Night":
                UpdateLighting(0f, 9000f, 0.3f, 0.5f);
                break;
        }

        timeSelected = true;                           
        timeContinueButton.gameObject.SetActive(true); // Enable continue once a choice is made
    }

    void StartTimer()
    {
        // Set player name before starting the logger
        if (performanceLogger != null)
        {
            performanceLogger.playerName = nameInputUI.nameInputField.text;
            performanceLogger.StartLogging();
        }

        // Only begin the countdown if the player has selected a time
        if (timeSelected)
        {
            timeSelectionCanvas.SetActive(false); // Hide time-selection UI
            menu.SetActive(true);                 // Show main menu again

            timerActive = true;                   // Start the timer
            timerText.gameObject.SetActive(true); // Display the timer text
        }
        else{Debug.LogWarning("You must select a time before continuing.");}
    }

    void UpdateLighting(float sunAngle, float temperature, float intensity, float indirectMultiplier)
    {
        // Adjust the environment's lighting if a directional light ("sun") is present
        if (RenderSettings.sun != null)
        {
            RenderSettings.sun.transform.rotation = Quaternion.Euler(
                Mathf.Lerp(-10, 50, sunAngle / 24f), -30, 0
            );
            RenderSettings.sun.colorTemperature = temperature;
            RenderSettings.sun.intensity = intensity;
            RenderSettings.sun.bounceIntensity = indirectMultiplier;
        }
    }

    void UpdateTimerUI()
    {
        // Format and display the remaining time as MM:SS
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = $"Time Left: {minutes:00}:{seconds:00}";
    }

    void ShowScenario2Message()
    {
        performanceLogger?.StopAndSave();
        // Notify the player that Scenario 2 is starting, then trigger it
        instructionText.text = "Scenario 2 is starting! Follow the tiger.";
        timerText.gameObject.SetActive(false);
        StartCoroutine(CloseCanvasAfterDelay(5f));
        //start scenario 2
        Object.FindAnyObjectByType<Scenario2Manager>()?.BeginScenario2();
        tiger.SetActive(true);
    }

    IEnumerator CloseCanvasAfterDelay(float delay)
    {
        // Wait, then hide the main menu canvas
        yield return new WaitForSeconds(delay);
        menu.SetActive(false);
    }
}
