using UnityEngine;
using System.Diagnostics;
using System.IO;

public class PerformanceLoggerScenario1 : MonoBehaviour
{
    [HideInInspector] public string playerName = "Player";

    // --- Internal performance metrics ---
    private float minFps = float.MaxValue;
    private float maxFps = float.MinValue;
    private float maxMemory = 0f;
    private long initialGC = 0;
    private float graphicsMemoryMB = 0f;
    private Stopwatch stopwatch;
    private bool logging = false;

    /// <summary>
    /// Starts collecting performance data.
    /// </summary>
    public void StartLogging()
    {
        initialGC = System.GC.GetTotalMemory(false);
        stopwatch = Stopwatch.StartNew();
        logging = true;
    }

    /// <summary>
    /// Stops the performance logging and saves the report to a .txt file.
    /// </summary>
    public void StopAndSave()
    {
        if (!logging) return;

        logging = false;
        stopwatch.Stop();

        long finalGC = System.GC.GetTotalMemory(false);
        float totalSeconds = stopwatch.ElapsedMilliseconds / 1000f;
        float gcDeltaMB = (finalGC - initialGC) / (1024f * 1024f);
        string timestamp = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        string report =
            "=== Scenario 1 Performance Report ===\n" +
            $"Date & Time: {timestamp}\n" +
            $"Total Duration: {totalSeconds:F1} seconds\n" +
            $"Min FPS: {minFps:F2}\n" +
            $"Max FPS: {maxFps:F2}\n" +
            $"Max Memory Usage: {maxMemory:F1} MB\n" +
            $"GC Delta: {gcDeltaMB:F1} MB\n" +
            $"Graphics Driver Memory: {graphicsMemoryMB:F1} MB\n" +
            "=================================\n";

        string safeName = string.IsNullOrWhiteSpace(playerName) ? "Player" : playerName.Trim();
        string fileName = $"{safeName}_PerfReport.txt";
        string filePath = Path.Combine(Application.persistentDataPath, fileName);

        try
        {
            File.WriteAllText(filePath, report);
            UnityEngine.Debug.Log($"[PERF] Report successfully saved.");
        }
        catch (System.Exception ex)
        {
            UnityEngine.Debug.LogError($"[PERF] Failed to write report: {ex.Message}");
        }
    }

    /// <summary>
    /// Collects performance data every frame while logging is active.
    /// </summary>
    void Update()
    {
        if (!logging) return;

        float fps = 1.0f / Time.deltaTime;
        if (fps < minFps) minFps = fps;
        if (fps > maxFps) maxFps = fps;

        float currentMemoryMB = System.GC.GetTotalMemory(false) / (1024f * 1024f);
        if (currentMemoryMB > maxMemory) maxMemory = currentMemoryMB;

        graphicsMemoryMB = UnityEngine.Profiling.Profiler.GetAllocatedMemoryForGraphicsDriver() / (1024f * 1024f);
    }
}
