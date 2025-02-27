using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ActivatGrapRay : MonoBehaviour
{
    // References to the left and right grab rays (the laser beams)
    public GameObject leftGrabRay;
    public GameObject rightGrabRay;

    // XRDirectInteractor components for detecting grabbing
    public XRDirectInteractor leftDirectGrab;
    public XRDirectInteractor rightDirectGrab;

    void Update()
    {
        // Check if either hand is grabbing an object
        bool isAnyHandGrabbing = leftDirectGrab.interactablesSelected.Count > 0 || rightDirectGrab.interactablesSelected.Count > 0;

        // Hide both grab rays if any hand is grabbing, show them when neither hand is grabbing
        leftGrabRay.SetActive(!isAnyHandGrabbing);
        rightGrabRay.SetActive(!isAnyHandGrabbing);
    }
}
