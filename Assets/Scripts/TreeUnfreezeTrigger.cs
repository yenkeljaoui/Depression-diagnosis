using UnityEngine;

public class TreeUnfreezeTrigger : MonoBehaviour
{
    public LakeFreezingTrigger lakeTrigger;      // Assign in Inspector
    public BeastHelpManager beastHelpManager;    // Assign in Inspector

    private bool hasUnfrozen = false;

    private void OnTriggerEnter(Collider other)
    {
        if (hasUnfrozen) return;

        if (other.CompareTag("Player") && other.transform.root.gameObject.name == "XR Origin (XR Rig)")
        {
            Debug.Log("Player touched the tree!");

            if (beastHelpManager != null && lakeTrigger != null)
            {
                if (beastHelpManager.DidPlayerChooseToHelp())
                {
                    lakeTrigger.UnfreezeLake();
                    hasUnfrozen = true;
                }
                else
                {
                    Debug.Log("Player didn’t agree to help – ignoring touch.");
                }
            }
            else
            {
                Debug.LogWarning("Missing BeastHelpManager or LakeFreezingTrigger reference.");
            }
        }
    }
}
