using UnityEngine;

public class LakeFreezingTrigger : MonoBehaviour
{
    public Material waterMaterial; // Assign in Inspector
    public Material iceMaterial;   // Assign in Inspector
    private Renderer lakeRenderer;
    private bool isFrozen = false; // Prevent multiple switches

    void Start()
    {
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
            isFrozen = true; // Prevents unnecessary updates
            Debug.Log("Lake is now frozen!");
        }
        else
        {
            Debug.LogError("Missing Ice Material or Renderer!");
        }
    }
}
