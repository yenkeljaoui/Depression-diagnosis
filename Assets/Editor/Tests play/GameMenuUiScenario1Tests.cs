using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TestTools;
using TMPro;

public class GameMenuUiScenario1Tests
{
    // Game objects and UI elements used in the tests
    private GameObject gameMenuObj;
    private GameMenuUiScenario1 gameMenu;
    private GameObject menu;
    private GameObject timeSelectionCanvas;
    private GameObject head;
    private Button continueButton;
    private Button noonButton;
    private Button eveningButton;
    private Button nightButton;
    private Button timeContinueButton;
    private TextMeshProUGUI instructionText;
    private TextMeshProUGUI timerText;

    /// <summary>
    /// Set up the test environment before each test runs.
    /// This initializes all required UI elements and components.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        // Simulate the XR Camera (Head)
        head = new GameObject("Head");
        head.transform.position = Vector3.zero;
        head.transform.forward = Vector3.forward;

        // Create the Game Menu UI (Main Menu)
        menu = new GameObject("Menu");
        menu.AddComponent<Canvas>();

        // Create the Time Selection UI
        timeSelectionCanvas = new GameObject("TimeSelectionCanvas");
        timeSelectionCanvas.AddComponent<Canvas>();

        // Create the main "Continue" button
        GameObject continueButtonObj = new GameObject("ContinueButton");
        continueButtonObj.transform.SetParent(menu.transform);
        continueButton = continueButtonObj.AddComponent<Button>();

        // Create time selection buttons (Noon, Evening, Night)
        GameObject noonButtonObj = new GameObject("NoonButton");
        noonButtonObj.transform.SetParent(timeSelectionCanvas.transform);
        noonButton = noonButtonObj.AddComponent<Button>();

        GameObject eveningButtonObj = new GameObject("EveningButton");
        eveningButtonObj.transform.SetParent(timeSelectionCanvas.transform);
        eveningButton = eveningButtonObj.AddComponent<Button>();

        GameObject nightButtonObj = new GameObject("NightButton");
        nightButtonObj.transform.SetParent(timeSelectionCanvas.transform);
        nightButton = nightButtonObj.AddComponent<Button>();

        // Create the Continue button inside Time Selection UI
        GameObject timeContinueButtonObj = new GameObject("TimeContinueButton");
        timeContinueButtonObj.transform.SetParent(timeSelectionCanvas.transform);
        timeContinueButton = timeContinueButtonObj.AddComponent<Button>();
        timeContinueButtonObj.SetActive(false); // Initially hidden

        // Create text elements (Instruction Text and Timer Text)
        GameObject instructionTextObj = new GameObject("InstructionText");
        instructionTextObj.transform.SetParent(menu.transform);
        instructionText = instructionTextObj.AddComponent<TextMeshProUGUI>();
        instructionText.text = "";

        GameObject timerTextObj = new GameObject("TimerText");
        timerTextObj.transform.SetParent(menu.transform);
        timerText = timerTextObj.AddComponent<TextMeshProUGUI>();
        timerText.text = "";

        // Create the main GameMenuUiScenario1 object and attach the script
        gameMenuObj = new GameObject("GameMenuUiScenario1Obj");
        gameMenu = gameMenuObj.AddComponent<GameMenuUiScenario1>();

        // Assign all references to the GameMenuUiScenario1 script
        gameMenu.menu = menu;
        gameMenu.timeSelectionCanvas = timeSelectionCanvas;
        gameMenu.head = head.transform;
        gameMenu.continueButton = continueButton;
        gameMenu.noonButton = noonButton;
        gameMenu.eveningButton = eveningButton;
        gameMenu.nightButton = nightButton;
        gameMenu.timeContinueButton = timeContinueButton;
        gameMenu.instructionText = instructionText;
        gameMenu.timerText = timerText;

        // Ensure that UI elements are in their expected initial states
        timeSelectionCanvas.SetActive(false);  // Time selection should be hidden
        timerText.gameObject.SetActive(false); // Timer should be hidden

        // Manually call Start() to initialize the script (since Unity won't do it automatically in tests)
        gameMenu.Start();
    }

    /// <summary>
    /// Clean up objects after each test to prevent memory leaks or interference between tests.
    /// </summary>
    [TearDown]
    public void Teardown()
    {
        Object.DestroyImmediate(gameMenuObj);
        Object.DestroyImmediate(menu);
        Object.DestroyImmediate(timeSelectionCanvas);
        Object.DestroyImmediate(head);
    }

    /// <summary>
    /// Verifies that the UI starts in the correct initial state.
    /// </summary>
    [Test]
    public void Test_InitialState()
    {
        // Timer text and time selection canvas should be hidden initially
        Assert.IsFalse(timerText.gameObject.activeSelf, "Timer should not be visible at the start.");
        Assert.IsFalse(timeSelectionCanvas.activeSelf, "Time selection UI should be hidden at the start.");
    }

    /// <summary>
    /// Tests whether clicking the Continue button correctly updates the tutorial steps.
    /// </summary>
    [Test]
    public void Test_NextStep_InstructionUpdate()
    {
        // Ensure that instruction text starts empty
        Assert.AreEqual("", instructionText.text, "Instruction text should be empty initially.");

        // First click should update the instructions
        continueButton.onClick.Invoke();
        Assert.AreEqual("To move, use the left joystick.", instructionText.text, "First step instruction mismatch.");

        // Second click should update to the next instruction
        continueButton.onClick.Invoke();
        Assert.AreEqual("To grab an object, press the grip button.", instructionText.text, "Second step instruction mismatch.");

        // Third click should transition to time selection
        continueButton.onClick.Invoke();
        Assert.AreEqual("You're ready! Select the time of day.", instructionText.text, "Third step instruction mismatch.");

        // The main continue button should now be hidden
        Assert.IsFalse(continueButton.gameObject.activeSelf, "Continue button should be hidden after last step.");

        // The time selection UI should now be active, and the menu should be deactivated
        Assert.IsTrue(timeSelectionCanvas.activeSelf, "Time selection UI should be visible.");
        Assert.IsFalse(menu.activeSelf, "Main menu should be hidden.");
    }

    /// <summary>
    /// Tests whether selecting a time of day correctly updates the UI state.
    /// </summary>
    [Test]
    public void Test_SelectTime_Noon()
    {
        // Activate the time selection UI
        timeSelectionCanvas.SetActive(true);

        // Simulate clicking the Noon button
        noonButton.onClick.Invoke();

        // Ensure that the Continue button inside the time selection UI is now active
        Assert.IsTrue(timeContinueButton.gameObject.activeSelf, "Continue button inside Time Selection UI should be active after selecting a time.");

        // Verify that 'timeSelected' was updated correctly using reflection
        var timeSelectedField = typeof(GameMenuUiScenario1)
            .GetField("timeSelected", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        bool timeSelected = (bool)timeSelectedField.GetValue(gameMenu);
        Assert.IsTrue(timeSelected, "Time selection was not properly recorded.");
    }

    /// <summary>
    /// Tests whether clicking the Continue button inside the Time Selection UI starts the timer.
    /// </summary>
    [Test]
    public void Test_StartTimer()
    {
        // Simulate selecting a time first
        timeSelectionCanvas.SetActive(true);
        noonButton.onClick.Invoke(); // Selects "Noon" and activates the continue button

        // Now simulate clicking the continue button inside Time Selection UI
        timeContinueButton.onClick.Invoke();

        // After starting the timer, the time selection UI should be hidden, and the main menu should be visible
        Assert.IsFalse(timeSelectionCanvas.activeSelf, "Time Selection UI should be hidden after pressing continue.");
        Assert.IsTrue(menu.activeSelf, "Main menu should be visible after starting the timer.");

        // The timer text should be active
        Assert.IsTrue(timerText.gameObject.activeSelf, "Timer should be visible after starting countdown.");

        // Verify that the timer was activated using reflection
        var timerActiveField = typeof(GameMenuUiScenario1)
            .GetField("timerActive", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        bool timerActive = (bool)timerActiveField.GetValue(gameMenu);
        Assert.IsTrue(timerActive, "Timer should be active after starting countdown.");
    }
}
