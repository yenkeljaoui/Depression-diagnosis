using UnityEngine;

public class LakeFreezingTrigger : MonoBehaviour
{
    public Material waterMaterial; // Assign in Inspector
    public Material iceMaterial;   // Assign in Inspector
    private Renderer lakeRenderer;
    private bool isFrozen = false; // Prevent multiple switches
    public BeastHelpManager helpManager; // Assign in Inspector
    public PlayerDataManager playerDataManager; // Assign in Inspector



    void Start()
    {
       // FindFirstObjectByType<Scenario3Manager>()?.BeginScenario3();
        lakeRenderer = GetComponent<Renderer>();

        if (lakeRenderer == null)
        {
            Debug.LogError("Renderer not found on " + gameObject.name);
        }
        else
        {
            Debug.Log("Lake material set to Water.");
            lakeRenderer.material = waterMaterial;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Object entered: " + other.gameObject.name); // Debug log

        if (!isFrozen && other.CompareTag("Animal")) // Only freeze if it's an animal
        {
            Debug.Log("Animal entered the lake, freezing it...");
            FreezeLake();
        }
    }
void FreezeLake()
{
    if (lakeRenderer != null && iceMaterial != null)
    {
        lakeRenderer.material = iceMaterial;
        isFrozen = true;
        Debug.Log("Lake is now frozen!");

        if (helpManager != null)
        {
            helpManager.ShowHelpDialog(); // Show dialog when lake is frozen
        }
        else
        {
            Debug.LogWarning("Help Manager not assigned.");
        }
    }
    else
    {
        Debug.LogError("Missing Ice Material or Renderer!");
    }
}




public void UnfreezeLake()
{
    if (lakeRenderer != null && waterMaterial != null)
    {
        lakeRenderer.material = waterMaterial;
        isFrozen = false;
        Debug.Log("Lake has been unfrozen!");

        // Log event
        if (playerDataManager != null)
        {
            playerDataManager.LogEvent("LakeUnfrozen", "Player touched the tree and unfroze the lake");
            Debug.Log("Player touched the tree and unfroze the lake");
            //begin scenario 3
            FindFirstObjectByType<Scenario3Manager>()?.BeginScenario3();

        }
        else
        {
            Debug.LogWarning("PlayerDataManager not assigned to LakeFreezingTrigger.");
        }
    }
    else
    {
        Debug.LogError("Missing Water Material or Renderer!");
    }
}



}
