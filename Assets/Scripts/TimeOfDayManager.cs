using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeOfDayManager : MonoBehaviour
{
    public Light sunLight; // The Sun (Directional Light)
    public TextMeshProUGUI timeText; // UI Text to display current time
    public Button noonButton, eveningButton, nightButton; // UI Buttons

    void Start()
    {
        // Assign button click functions
        if (noonButton != null) 
            noonButton.onClick.AddListener(SetTimeToNoon);

        if (eveningButton != null) 
            eveningButton.onClick.AddListener(SetTimeToEvening);

        if (nightButton != null) 
            nightButton.onClick.AddListener(SetTimeToNight);
    }

    // Set time to Noon (Super Bright Sunlight)
    public void SetTimeToNoon()
    {
        UpdateLighting(12f, 7000f, 5.0f, 1.5f); // Higher intensity and cooler daylight
        timeText.text = "Current Time: Noon"; 
    }

    // Set time to Evening
    public void SetTimeToEvening()
    {
        UpdateLighting(18f, 3500f, 1.5f, 1.0f); // Warm sunset light
        timeText.text = "Current Time: Evening"; 
    }

    // Set time to Night
    public void SetTimeToNight()
    {
        UpdateLighting(0f, 9000f, 0.3f, 0.5f); // Dark blue moonlight
        timeText.text = "Current Time: Night"; 
    }

    // Update the Sun (Directional Light) based on time of day
    public void UpdateLighting(float sunAngle, float temperature, float intensity, float indirectMultiplier)
    {
        if (sunLight != null)
        {
            // Rotate the sun to match the selected time
            sunLight.transform.rotation = Quaternion.Euler(
                Mathf.Lerp(-10, 50, sunAngle / 24f), // X-axis: Sun height in sky
                -30, // Y-axis (keep direction)
                0 // Z-axis
            );

            // Change the sunlight color temperature
            sunLight.colorTemperature = temperature;

            // Adjust sunlight brightness
            sunLight.intensity = intensity;

            // Adjust indirect light bounce for better brightness
            sunLight.bounceIntensity = indirectMultiplier;
        }
    }
}
