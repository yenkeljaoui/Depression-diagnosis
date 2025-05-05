using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRGrabInteractable))]
public class GuitarPlaySound : MonoBehaviour
{
    public AudioSource grabSound;

    void Start()
    {
        XRGrabInteractable grab = GetComponent<XRGrabInteractable>();
        if (grab != null)
        {
            grab.selectEntered.AddListener(_ =>
            {
                if (grabSound != null)
                    grabSound.Play();
            });
        }
    }
}
