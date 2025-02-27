using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections; // Needed for IEnumerator and coroutines

public class GameMenuUiScenario1 : MonoBehaviour
{
    public GameObject menu; // Main UI panel (Game Menu)
    public GameObject timeSelectionCanvas; // The Time Selection UI panel
    public Transform head; // XR Camera (Head)
    public float spwnDistance = 10f; // Distance in front of the player
    public float smoothSpeed = 5f; // UI movement speed

    public Button continueButton; // The main continue button (for step tutorial)
    public Button noonButton, eveningButton, nightButton; // Time selection buttons
    public Button timeContinueButton; // The new Continue button in TimeSelectionCanvas
    public TextMeshProUGUI instructionText; // Instruction text
    public TextMeshProUGUI timerText; // Timer text

    private int step = 0; // Tracks tutorial steps
    private bool timerActive = false; // Determines if the timer should be running
    private float timeRemaining = 3f; // 2 minutes countdown
    private bool timeSelected = false; // Prevents the timer from starting before selection

    public void Start()
    {
        // Assign the tutorial continue button
        if (continueButton != null)
            continueButton.onClick.AddListener(NextStep);
        else
            Debug.LogError("Continue Button is not assigned!");

        // Hide the timer and Time Selection UI at the start
        timerText.gameObject.SetActive(false);
        timeSelectionCanvas.SetActive(false);

        // Assign time selection buttons
        if (noonButton != null) noonButton.onClick.AddListener(() => SelectTime("Noon"));
        if (eveningButton != null) eveningButton.onClick.AddListener(() => SelectTime("Evening"));
        if (nightButton != null) nightButton.onClick.AddListener(() => SelectTime("Night"));

        // Assign the new Continue button inside TimeSelectionCanvas
        if (timeContinueButton != null)
            timeContinueButton.onClick.AddListener(StartTimer);
    }

    public void Update()
    {
        if (head == null) return;

        // Update the Game Menu UI position if it's active
        if (menu.activeSelf)
        {
            Vector3 targetPosition = head.position + head.forward * spwnDistance;
            targetPosition.y = head.position.y;
            menu.transform.position = Vector3.Lerp(menu.transform.position, targetPosition, Time.deltaTime * smoothSpeed);

            Vector3 lookDirection = head.position - menu.transform.position;
            lookDirection.y = 0;
            menu.transform.rotation = Quaternion.LookRotation(-lookDirection);
        }

        // Update the Time Selection Canvas position if it's active
        if (timeSelectionCanvas.activeSelf)
        {
            Vector3 targetTimePosition = head.position + head.forward * spwnDistance;
            targetTimePosition.y = head.position.y;
            timeSelectionCanvas.transform.position = Vector3.Lerp(timeSelectionCanvas.transform.position, targetTimePosition, Time.deltaTime * smoothSpeed);

            Vector3 timeLookDirection = head.position - timeSelectionCanvas.transform.position;
            timeLookDirection.y = 0;
            timeSelectionCanvas.transform.rotation = Quaternion.LookRotation(-timeLookDirection);
        }

        // Handle the countdown timer after time is selected
        if (timerActive && timeSelected)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimerUI();
            }
            else
            {
                timerActive = false;
                timeRemaining = 0;
                ShowScenario2Message();
            }
        }
    }

    // Called when the tutorial continue button is pressed
    void NextStep()
    {
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
                instructionText.text = "You're ready! Select the time of day.";
                continueButton.gameObject.SetActive(false); // Hide the main continue button

                // Show the Time Selection UI and hide the Game Menu UI
                timeSelectionCanvas.SetActive(true);
                menu.SetActive(false);
                break;
        }
    }

    // Called when a time is selected (Noon, Evening, or Night)
    void SelectTime(string timeOfDay)
    {
        // Update the lighting based on the selected time
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

        // Mark that a time was selected
        timeSelected = true;

        // Show the Continue button inside Time Selection Canvas after a time is selected
        timeContinueButton.gameObject.SetActive(true);
    }

    // Called when the Continue button in Time Selection Canvas is pressed
    void StartTimer()
    {
        if (timeSelected)
        {
            // Hide the Time Selection UI
            timeSelectionCanvas.SetActive(false);
            // Bring back the Game Menu UI (which now will show the timer)
            menu.SetActive(true);

            // Start the timer and show the timer text
            timerActive = true;
            timerText.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning("You must select a time before continuing.");
        }
    }

    // Update the sun's lighting based on the given parameters
    void UpdateLighting(float sunAngle, float temperature, float intensity, float indirectMultiplier)
    {
        if (RenderSettings.sun != null)
        {
            // Rotate the sun
            RenderSettings.sun.transform.rotation = Quaternion.Euler(
                Mathf.Lerp(-10, 50, sunAngle / 24f), -30, 0
            );

            // Adjust the lighting properties
            RenderSettings.sun.colorTemperature = temperature;
            RenderSettings.sun.intensity = intensity;
            RenderSettings.sun.bounceIntensity = indirectMultiplier;
        }
    }

    // Update the timer text UI
    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = $"Time Left: {minutes:00}:{seconds:00}";
    }

    // Called when the timer finishes
    void ShowScenario2Message()
    {
        instructionText.text = "Scenario 2 is starting!";
        timerText.gameObject.SetActive(false);
        // Start a coroutine to close the Game Menu UI after 5 seconds
        StartCoroutine(CloseCanvasAfterDelay(5f));
    }

    // Coroutine: Wait for the specified delay, then close the Game Menu UI
    System.Collections.IEnumerator CloseCanvasAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        // Close (disable) the Game Menu UI canvas
        menu.SetActive(false);
    }
}
