
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class ForestDemoSceneTests
{
    private const string sceneName = "Forest Demo Scene";
    private string fallbackSceneName;

    [SetUp]
    public void Setup()
    {
        // Store the name of the currently active scene as a fallback.
        Scene activeScene = SceneManager.GetActiveScene();
        fallbackSceneName = activeScene.name; // This might be empty if the active scene is not properly set.
    }

    [UnityTest]
    public IEnumerator ForestDemoScene_LoadsSuccessfully()
    {
        // Load the Forest Demo Scene in Single mode (this replaces any currently loaded scenes)
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        
        // Wait until the scene is fully loaded
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        
        // Verify that the scene is loaded
        Scene loadedScene = SceneManager.GetSceneByName(sceneName);
        Assert.IsTrue(loadedScene.isLoaded, "The Forest Demo Scene should be loaded.");
        
        
        // Cleanup: if a fallback scene name was recorded, load that scene
        if (!string.IsNullOrEmpty(fallbackSceneName))
        {
            yield return SceneManager.LoadSceneAsync(fallbackSceneName, LoadSceneMode.Single);
        }
        else
        {
            Debug.LogWarning("Fallback scene name is empty. Cleanup step skipped.");
        }
    }
} 