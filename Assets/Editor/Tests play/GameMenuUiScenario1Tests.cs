// using System.Collections;
// using NUnit.Framework;
// using UnityEngine;
// using UnityEngine.UI;
// using UnityEngine.TestTools;
// using TMPro;

// public class GameMenuUiScenario1Tests
// {
//     // Game objects and UI elements used in the tests
//     private GameObject gameMenuObj;
//     private GameMenuUiScenario1 gameMenu;
//     private GameObject menu;
//     private GameObject timeSelectionCanvas;
//     private GameObject head;
//     private Button continueButton;
//     private Button noonButton;
//     private Button eveningButton;
//     private Button nightButton;
//     private Button timeContinueButton;
//     private TextMeshProUGUI instructionText;
//     private TextMeshProUGUI timerText;

//     /// <summary>
//     /// Set up the test environment before each test runs.
//     /// This initializes all required UI elements and components.
//     /// </summary>
//     [SetUp]
// public void Setup()
// {
//     // -- Head camera (XR head)
//     head = new GameObject("Head");
//     head.transform.position = Vector3.zero;
//     head.transform.forward = Vector3.forward;

//     // -- Main menu UI
//     menu = new GameObject("Menu");
//     menu.AddComponent<Canvas>();

//     // -- Time selection UI
//     timeSelectionCanvas = new GameObject("TimeSelectionCanvas");
//     timeSelectionCanvas.AddComponent<Canvas>();

//     // -- Tutorial continue button
//     GameObject continueButtonObj = new GameObject("ContinueButton");
//     continueButtonObj.transform.SetParent(menu.transform);
//     continueButton = continueButtonObj.AddComponent<Button>();

//     // -- Time selection buttons
//     noonButton = new GameObject("NoonButton").AddComponent<Button>();
//     noonButton.transform.SetParent(timeSelectionCanvas.transform);

//     eveningButton = new GameObject("EveningButton").AddComponent<Button>();
//     eveningButton.transform.SetParent(timeSelectionCanvas.transform);

//     nightButton = new GameObject("NightButton").AddComponent<Button>();
//     nightButton.transform.SetParent(timeSelectionCanvas.transform);

//     // -- Continue button inside time selection
//     timeContinueButton = new GameObject("TimeContinueButton").AddComponent<Button>();
//     timeContinueButton.transform.SetParent(timeSelectionCanvas.transform);
//     timeContinueButton.gameObject.SetActive(false); // hidden at start

//     // -- Instruction text
//     instructionText = new GameObject("InstructionText").AddComponent<TextMeshProUGUI>();
//     instructionText.transform.SetParent(menu.transform);
//     instructionText.text = "";

//     // -- Timer text
//     timerText = new GameObject("TimerText").AddComponent<TextMeshProUGUI>();
//     timerText.transform.SetParent(menu.transform);
//     timerText.text = "";

//     // -- nameInputCanvas
//     GameObject nameInputCanvas = new GameObject("NameInputCanvas");
//     nameInputCanvas.AddComponent<Canvas>();

//     // -- nameInputUI and TMP_InputField
//     GameObject nameInputUIObj = new GameObject("NameInputUI");
//     PlayerNameInputUI nameInputUI = nameInputUIObj.AddComponent<PlayerNameInputUI>();

//     GameObject nameInputFieldObj = new GameObject("NameInputField");
//     TMP_InputField inputField = nameInputFieldObj.AddComponent<TMP_InputField>();
//     nameInputFieldObj.transform.SetParent(nameInputUIObj.transform);
//     nameInputUI.nameInputField = inputField;

//     // -- Start Game Button
//     GameObject startButtonObj = new GameObject("StartGameButton");
//     Button startButton = startButtonObj.AddComponent<Button>();
//     startButtonObj.transform.SetParent(nameInputUIObj.transform);
//     nameInputUI.startGameButton = startButton;

//     // parent nameInputUI to canvas
//     nameInputUIObj.transform.SetParent(nameInputCanvas.transform);

//     // -- Performance logger
//     GameObject loggerObj = new GameObject("PerformanceLogger");
//     PerformanceLoggerScenario1 performanceLogger = loggerObj.AddComponent<PerformanceLoggerScenario1>();

//     // -- GameMenuUiScenario1 instance
//     gameMenuObj = new GameObject("GameMenuUiScenario1Obj");
//     gameMenu = gameMenuObj.AddComponent<GameMenuUiScenario1>();

//     // -- Assign all references
//     gameMenu.head = head.transform;
//     gameMenu.menu = menu;
//     gameMenu.timeSelectionCanvas = timeSelectionCanvas;
//     gameMenu.continueButton = continueButton;
//     gameMenu.noonButton = noonButton;
//     gameMenu.eveningButton = eveningButton;
//     gameMenu.nightButton = nightButton;
//     gameMenu.timeContinueButton = timeContinueButton;
//     gameMenu.instructionText = instructionText;
//     gameMenu.timerText = timerText;
//     gameMenu.nameInputUI = nameInputUI;
//     gameMenu.nameInputCanvas = nameInputCanvas;
//     gameMenuObj.AddComponent<PerformanceLoggerScenario1>(); // in case it uses GetComponent internally
//     gameMenu.performanceLogger = performanceLogger;

//     // -- Initial UI states
//     timeSelectionCanvas.SetActive(false);
//     timerText.gameObject.SetActive(false);

//     // -- Call Start manually
//     gameMenu.Start();
// }


//     /// <summary>
//     /// Clean up objects after each test to prevent memory leaks or interference between tests.
//     /// </summary>
//     [TearDown]
//     public void Teardown()
//     {
//         Object.DestroyImmediate(gameMenuObj);
//         Object.DestroyImmediate(menu);
//         Object.DestroyImmediate(timeSelectionCanvas);
//         Object.DestroyImmediate(head);
//     }

//     /// <summary>
//     /// Verifies that the UI starts in the correct initial state.
//     /// </summary>
//     [Test]
//     public void Test_InitialState()
//     {
//         // Timer text and time selection canvas should be hidden initially
//         Assert.IsFalse(timerText.gameObject.activeSelf, "Timer should not be visible at the start.");
//         Assert.IsFalse(timeSelectionCanvas.activeSelf, "Time selection UI should be hidden at the start.");
//     }

//     /// <summary>
//     /// Tests whether clicking the Continue button correctly updates the tutorial steps.
//     /// </summary>
//     [Test]
//     public void Test_NextStep_InstructionUpdate()
//     {
//         // Ensure that instruction text starts empty
//         Assert.AreEqual("", instructionText.text, "Instruction text should be empty initially.");

//         // First click should update the instructions
//         continueButton.onClick.Invoke();
//         Assert.AreEqual("To move, use the left joystick.", instructionText.text, "First step instruction mismatch.");

//         // Second click should update to the next instruction
//         continueButton.onClick.Invoke();
//         Assert.AreEqual("To grab an object, press the grip button.", instructionText.text, "Second step instruction mismatch.");

//         // Third click should transition to time selection
//         continueButton.onClick.Invoke();
//         Assert.AreEqual("All set, enjoy the  adventure..", instructionText.text, "Third step instruction mismatch.");

//         // The main continue button should now be hidden
//         Assert.IsFalse(continueButton.gameObject.activeSelf, "Continue button should be hidden after last step.");

//         // The time selection UI should now be active, and the menu should be deactivated
//         Assert.IsTrue(timeSelectionCanvas.activeSelf, "Time selection UI should be visible.");
//         Assert.IsFalse(menu.activeSelf, "Main menu should be hidden.");
//     }

//     /// <summary>
//     /// Tests whether selecting a time of day correctly updates the UI state.
//     /// </summary>
//     [Test]
//     public void Test_SelectTime_Noon()
//     {
//         // Activate the time selection UI
//         timeSelectionCanvas.SetActive(true);

//         // Simulate clicking the Noon button
//         noonButton.onClick.Invoke();

//         // Ensure that the Continue button inside the time selection UI is now active
//         Assert.IsTrue(timeContinueButton.gameObject.activeSelf, "Continue button inside Time Selection UI should be active after selecting a time.");

//         // Verify that 'timeSelected' was updated correctly using reflection
//         var timeSelectedField = typeof(GameMenuUiScenario1)
//             .GetField("timeSelected", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
//         bool timeSelected = (bool)timeSelectedField.GetValue(gameMenu);
//         Assert.IsTrue(timeSelected, "Time selection was not properly recorded.");
//     }

//     /// <summary>
//     /// Tests whether clicking the Continue button inside the Time Selection UI starts the timer.
//     /// </summary>
//     [Test]
//     public void Test_StartTimer()
//     {
//         // Simulate selecting a time first
//         timeSelectionCanvas.SetActive(true);
//         noonButton.onClick.Invoke(); // Selects "Noon" and activates the continue button

//         // Now simulate clicking the continue button inside Time Selection UI
//         timeContinueButton.onClick.Invoke();

//         // After starting the timer, the time selection UI should be hidden, and the main menu should be visible
//         Assert.IsFalse(timeSelectionCanvas.activeSelf, "Time Selection UI should be hidden after pressing continue.");
//         Assert.IsTrue(menu.activeSelf, "Main menu should be visible after starting the timer.");

//         // The timer text should be active
//         Assert.IsTrue(timerText.gameObject.activeSelf, "Timer should be visible after starting countdown.");

//         // Verify that the timer was activated using reflection
//         var timerActiveField = typeof(GameMenuUiScenario1)
//             .GetField("timerActive", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
//         bool timerActive = (bool)timerActiveField.GetValue(gameMenu);
//         Assert.IsTrue(timerActive, "Timer should be active after starting countdown.");
//     }
// }


using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TestTools;
using TMPro;

public class GameMenuUiScenario1Tests
{
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

    [SetUp]
    public void Setup()
    {
        // Head
        head = new GameObject("Head");
        head.transform.position = Vector3.zero;
        head.transform.forward = Vector3.forward;

        // Menu
        menu = new GameObject("Menu");
        menu.AddComponent<Canvas>();

        // Time selection canvas
        timeSelectionCanvas = new GameObject("TimeSelectionCanvas");
        timeSelectionCanvas.AddComponent<Canvas>();

        // Tutorial Continue Button
        GameObject continueButtonObj = new GameObject("ContinueButton");
        continueButtonObj.transform.SetParent(menu.transform);
        continueButton = continueButtonObj.AddComponent<Button>();

        // Time selection buttons
        noonButton = new GameObject("NoonButton").AddComponent<Button>();
        noonButton.transform.SetParent(timeSelectionCanvas.transform);

        eveningButton = new GameObject("EveningButton").AddComponent<Button>();
        eveningButton.transform.SetParent(timeSelectionCanvas.transform);

        nightButton = new GameObject("NightButton").AddComponent<Button>();
        nightButton.transform.SetParent(timeSelectionCanvas.transform);

        timeContinueButton = new GameObject("TimeContinueButton").AddComponent<Button>();
        timeContinueButton.transform.SetParent(timeSelectionCanvas.transform);
        timeContinueButton.gameObject.SetActive(false);

        // Instruction text
        instructionText = new GameObject("InstructionText").AddComponent<TextMeshProUGUI>();
        instructionText.transform.SetParent(menu.transform);
        instructionText.text = "";

        // Timer text
        timerText = new GameObject("TimerText").AddComponent<TextMeshProUGUI>();
        timerText.transform.SetParent(menu.transform);
        timerText.text = "";

        // nameInputCanvas
        GameObject nameInputCanvas = new GameObject("NameInputCanvas");
        nameInputCanvas.AddComponent<Canvas>();

        // nameInputUI
        GameObject nameInputUIObj = new GameObject("NameInputUI");
        PlayerNameInputUI nameInputUI = nameInputUIObj.AddComponent<PlayerNameInputUI>();

        GameObject nameInputFieldObj = new GameObject("NameInputField");
        TMP_InputField inputField = nameInputFieldObj.AddComponent<TMP_InputField>();
        nameInputFieldObj.transform.SetParent(nameInputUIObj.transform);
        nameInputUI.nameInputField = inputField;

        GameObject startButtonObj = new GameObject("StartGameButton");
        Button startButton = startButtonObj.AddComponent<Button>();
        startButtonObj.transform.SetParent(nameInputUIObj.transform);
        nameInputUI.startGameButton = startButton;

        nameInputUIObj.transform.SetParent(nameInputCanvas.transform);

        // Performance logger
        GameObject loggerObj = new GameObject("PerformanceLogger");
        PerformanceLoggerScenario1 performanceLogger = loggerObj.AddComponent<PerformanceLoggerScenario1>();

        // Game object for block1
        GameObject block1 = new GameObject("Block1");
        block1.SetActive(false);

        // Data manager
        GameObject dataManagerObj = new GameObject("DataManager");
        PlayerDataManager dataManager = dataManagerObj.AddComponent<PlayerDataManager>();
        dataManager.sessionData = new PlayerSessionData(); // ← שורת התיקון

        // GameMenuUiScenario1 object
        gameMenuObj = new GameObject("GameMenuUiScenario1Obj");
        gameMenu = gameMenuObj.AddComponent<GameMenuUiScenario1>();

        // Assign references
        gameMenu.head = head.transform;
        gameMenu.menu = menu;
        gameMenu.timeSelectionCanvas = timeSelectionCanvas;
        gameMenu.continueButton = continueButton;
        gameMenu.noonButton = noonButton;
        gameMenu.eveningButton = eveningButton;
        gameMenu.nightButton = nightButton;
        gameMenu.timeContinueButton = timeContinueButton;
        gameMenu.instructionText = instructionText;
        gameMenu.timerText = timerText;
        gameMenu.nameInputUI = nameInputUI;
        gameMenu.nameInputCanvas = nameInputCanvas;
        gameMenu.performanceLogger = performanceLogger;
        gameMenu.block1 = block1;
        typeof(GameMenuUiScenario1).GetField("dataManager", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .SetValue(gameMenu, dataManager);

        // Init state
        timeSelectionCanvas.SetActive(false);
        timerText.gameObject.SetActive(false);

        // Call Start()
        gameMenu.Start();
    }

    [TearDown]
    public void Teardown()
    {
        Object.DestroyImmediate(gameMenuObj);
        Object.DestroyImmediate(menu);
        Object.DestroyImmediate(timeSelectionCanvas);
        Object.DestroyImmediate(head);
    }

    [Test]
    public void Test_InitialState()
    {
        Assert.IsFalse(timerText.gameObject.activeSelf);
        Assert.IsFalse(timeSelectionCanvas.activeSelf);
    }

    [Test]
    public void Test_NextStep_InstructionUpdate()
    {
        Assert.AreEqual("", instructionText.text);

        continueButton.onClick.Invoke();
        Assert.AreEqual("To move, use the joystick.", instructionText.text); // Updated per real string

        continueButton.onClick.Invoke();
        Assert.AreEqual("To grab an object, press the grip button.", instructionText.text);

        continueButton.onClick.Invoke();
        Assert.AreEqual("All set! Feel free to walk around the forest till the timer up there stops. :)", instructionText.text);

        Assert.IsFalse(continueButton.gameObject.activeSelf);
        Assert.IsTrue(timeSelectionCanvas.activeSelf);
        Assert.IsFalse(menu.activeSelf);
    }

    [Test]
    public void Test_SelectTime_Noon()
    {
        timeSelectionCanvas.SetActive(true);
        noonButton.onClick.Invoke();

        Assert.IsTrue(timeContinueButton.gameObject.activeSelf);

        var timeSelectedField = typeof(GameMenuUiScenario1)
            .GetField("timeSelected", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        bool timeSelected = (bool)timeSelectedField.GetValue(gameMenu);
        Assert.IsTrue(timeSelected);
    }

    [Test]
    public void Test_StartTimer()
    {
        timeSelectionCanvas.SetActive(true);
        noonButton.onClick.Invoke();

        timeContinueButton.onClick.Invoke();

        Assert.IsFalse(timeSelectionCanvas.activeSelf);
        Assert.IsTrue(menu.activeSelf);
        Assert.IsTrue(timerText.gameObject.activeSelf);

        var timerActiveField = typeof(GameMenuUiScenario1)
            .GetField("timerActive", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        bool timerActive = (bool)timerActiveField.GetValue(gameMenu);
        Assert.IsTrue(timerActive);
    }
}
