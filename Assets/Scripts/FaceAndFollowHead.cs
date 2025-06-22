using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class CanvasFaceHead : MonoBehaviour
{
    [Tooltip("Reference to the player's head (XR Camera Transform)")]
    public Transform head;

    [Tooltip("Distance in front of the player")]
    public float spwnDistance = 10f;

    [Tooltip("Speed at which the canvas moves into position")]
    public float smoothSpeed = 5f;

    RectTransform rectTransform;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        if (head == null)
            Debug.LogError($"{name}: Head reference is not set on CanvasFaceHead.");
    }

    void Update()
    {
        if (head == null) return;

        // Calculate target position directly in front of the head
        Vector3 targetPosition = head.position + head.forward * spwnDistance;
        targetPosition.y = head.position.y;  // keep same vertical level
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smoothSpeed);

        // Calculate rotation so canvas always faces the head
        Vector3 directionToHead = head.position - transform.position;
        directionToHead.y = 0;  // ignore vertical tilt
        transform.rotation = Quaternion.LookRotation(-directionToHead);
    }
}
