// using UnityEngine;
// using UnityEngine.XR.Interaction.Toolkit;

// public class InteractableObject : MonoBehaviour
// {
//     // Identifier for the object (e.g., "flower", "stone")
//     public string objectType;

//     // Reference to the XR Grab Interactable component
//     private XRGrabInteractable grabInteractable;

//     void Start()
//     {
//         // Try to get the XRGrabInteractable component from this GameObject
//         grabInteractable = GetComponent<XRGrabInteractable>();

//         if (grabInteractable != null)
//         {
//             // Subscribe to the selectEntered event, which is triggered when the object is grabbed
//             grabInteractable.selectEntered.AddListener(OnSelectEntered);
//         }
//         else
//         {
//             Debug.LogWarning("XRGrabInteractable component not found on " + gameObject.name);
//         }
//     }

//     void OnDestroy()
//     {
//         // Unsubscribe to prevent potential memory leaks
//         if (grabInteractable != null)
//         {
//             grabInteractable.selectEntered.RemoveListener(OnSelectEntered);
//         }
//     }

//     // Event handler that is called when the object is grabbed
//     private void OnSelectEntered(SelectEnterEventArgs args)
//     {
//         OnInteract();
//     }

//     // Custom method to log the interaction
//     public void OnInteract()
//     {
//         // Find the PlayerDataManager in the scene
//         PlayerDataManager dataManager = Object.FindFirstObjectByType<PlayerDataManager>();

//         if (dataManager != null)
//         {
//             dataManager.LogEvent("InteractObject", "Interacted with " + objectType);
//         }
//         else
//         {
//             Debug.LogWarning("PlayerDataManager not found in the scene!");
//         }
//     }
// }




using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class InteractableObject : MonoBehaviour
{
    // Identifier for the object (e.g., "flower", "stone")
    public string objectType;

    // Reference to the XR Grab Interactable component
    private XRGrabInteractable grabInteractable;

    void Start()
    {
        // Try to get the XRGrabInteractable component from this GameObject
        grabInteractable = GetComponent<XRGrabInteractable>();

        if (grabInteractable != null)
        {
            // Subscribe to the selectEntered event, which is triggered when the object is grabbed
            grabInteractable.selectEntered.AddListener(OnSelectEntered);
        }
        else
        {
            Debug.LogWarning("XRGrabInteractable component not found on " + gameObject.name);
        }
    }

    void OnDestroy()
    {
        // Unsubscribe to prevent potential memory leaks
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.RemoveListener(OnSelectEntered);
        }
    }

    // Event handler that is called when the object is grabbed
    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        OnInteract();
    }

    // Custom method to log the interaction
    public void OnInteract()
    {
        // Find the PlayerDataManager in the scene
        PlayerDataManager dataManager = Object.FindFirstObjectByType<PlayerDataManager>();

        if (dataManager != null)
        {
            dataManager.LogEvent("InteractObject", "Interacted with " + objectType);
        }
        else
        {
            Debug.LogWarning("PlayerDataManager not found in the scene!");
        }
    }
}
