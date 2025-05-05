using UnityEngine;
using System.Collections;
using UnityEngine.XR.Interaction.Toolkit;

public class Scenario2Manager : MonoBehaviour
{
    [Header("References")]
    [Tooltip("Drag in the root Transform of your XR Rig (XR Origin)")]
    public Transform xrRigRoot;

    [Tooltip("Empty GameObject marking the Scenario 2 start point")]
    public Transform scenario2StartPoint;

    // internal cached components
    CharacterController cc;
    ContinuousMoveProviderBase mover;
    Rigidbody rb;

    void Awake()
    {
        // cache the CharacterController on the root
        cc    = xrRigRoot.GetComponent<CharacterController>();
        // cache the ContinuousMoveProvider (applies gravity)
        mover = xrRigRoot.GetComponentInChildren<ContinuousMoveProviderBase>();
        // cache an optional Rigidbody if you have one
        rb    = xrRigRoot.GetComponent<Rigidbody>();

        if (cc == null)
            Debug.LogError($"[Scenario2Manager] No CharacterController found under '{xrRigRoot.name}'");
    }

    /// <summary>
    /// Call this method to teleport the player into Scenario 2.
    /// </summary>
    public void BeginScenario2()
    {
        StartCoroutine(TeleportToScenario2());
    }

    private IEnumerator TeleportToScenario2()
    {
        // 1) Temporarily disable character movement & physics
        if (mover != null) mover.enabled = false;
        if (cc    != null) cc.enabled    = false;
        if (rb    != null)
        {
            rb.isKinematic     = true;
            rb.velocity        = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        // 2) Raycast down from 5 m above the start point to find the ground Y
        Vector3 dest = scenario2StartPoint.position;
        if (Physics.Raycast(dest + Vector3.up * 5f, Vector3.down, out RaycastHit hit, 20f))
        {
            // compute offset so the bottom of the CC capsule sits exactly on the ground
            float bottomOffset = cc.height * 0.5f + cc.center.y;
            dest.y = hit.point.y + bottomOffset;
        }
        else
        {
            Debug.LogWarning("[Scenario2Manager] Raycast failed – using raw startPoint Y");
        }

        // 3) Snap the XR Rig root to the corrected destination
        xrRigRoot.position = dest;

        // 4) Wait a physics step so the CC can re-evaluate isGrounded
        yield return new WaitForFixedUpdate();

        // 5) Re-enable physics & movement
        if (rb    != null) rb.isKinematic = false;
        if (cc    != null) cc.enabled     = true;
        if (mover != null) mover.enabled  = true;
    }
}

