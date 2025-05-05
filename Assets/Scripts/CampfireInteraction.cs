using UnityEngine;

public class CampfireInteraction : MonoBehaviour
{
    [Tooltip("The GameObject with your fire ParticleSystem")]
    public GameObject fireEffect;

    [Tooltip("The Canvas GameObject showing instructions")]
    public GameObject instructionUI;

    private bool isLit = false;
    private PlayerDataManager dataManager;

    void Start()
    {
        // Start with fire off
        if (fireEffect != null)
            fireEffect.SetActive(false);

        // Find the data manager
        dataManager = FindFirstObjectByType<PlayerDataManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        // Only light once, and only when collided by the torch
        if (isLit) return;

        if (other.CompareTag("Torch"))
        {
            isLit = true;

            if (fireEffect != null)
                fireEffect.SetActive(true);

            if (instructionUI != null)
                instructionUI.SetActive(false);

            if (dataManager != null)
            {
                dataManager.LogEvent("Campfire", "Campfire was lit by the torch");
            }
            else
            {
                Debug.LogWarning("PlayerDataManager not found!");
            }
        }
    }
}
